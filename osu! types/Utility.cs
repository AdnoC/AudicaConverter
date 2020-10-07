﻿using AudicaTools;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using osutoaudica;

namespace OsuTypes
{
    internal static class OsuUtility
    {
        public static float MsToTick(float ms, List<TimingPoint> timingPoints, int roundingPrecision = 1)
        {
            //Timing points is assumed to be a list of timing points sorted in chronological order.
            float tickTime = 0f;
            int i = 0;
            while (i + 1 < timingPoints.Count && ms >= timingPoints[i + 1].ms)
            {
                tickTime += (timingPoints[i + 1].ms - timingPoints[i].ms) * 480f / (float)timingPoints[i].beatTime;
                i++;
            }
            return (float)Math.Round(tickTime) + (float)Math.Round((ms - timingPoints[i].ms) * 480f / (float)timingPoints[i].beatTime / roundingPrecision) * roundingPrecision;
        }

        public static float ticksSinceLastTimingPoint(float tick, List<TimingPoint> timingPoints)
        {
            int timingPointIndex = timingPoints.FindIndex(tp => tp.audicaTick > tick) - 1;
            if (timingPointIndex == -2) timingPointIndex = timingPoints.Count - 1;
            return tick - timingPoints[timingPointIndex].audicaTick;
        }

        public static float CalculateSliderDuration(HitObject hitObject, float globalSliderVelocity, List<TimingPoint> mergedTimingPoints)
        {
            float time = hitObject.time;
            float remainingPixelLength = hitObject.pixelLength * hitObject.repeats;
            int prevTimingPointIdx = mergedTimingPoints.FindIndex(tp => tp.ms >= hitObject.time) - 1;
            if (prevTimingPointIdx < 0) prevTimingPointIdx = mergedTimingPoints.Count() - 1;

            while (remainingPixelLength > 0)
            {
                TimingPoint prevTimingPoint = mergedTimingPoints[prevTimingPointIdx];
                TimingPoint nextTimingPoint = prevTimingPointIdx + 1 < mergedTimingPoints.Count ? mergedTimingPoints[prevTimingPointIdx + 1] : null;
                //Try finishing the slider with the current sv/beatTime
                float timeToFinish = remainingPixelLength / (100 * globalSliderVelocity * prevTimingPoint.sliderVelocity) * (float)prevTimingPoint.beatTime;
                if (nextTimingPoint == null || time + timeToFinish <= nextTimingPoint.ms)
                {
                    //Slider complete
                    time += timeToFinish;
                    remainingPixelLength = 0;
                }
                else
                {
                    //Slider not finished before next tick. Calculate how far it got
                    remainingPixelLength -= (nextTimingPoint.ms - time) / (float)prevTimingPoint.beatTime * 100f * globalSliderVelocity * prevTimingPoint.sliderVelocity;
                    time = nextTimingPoint.ms;
                    prevTimingPointIdx++;
                }
            }
            return time - hitObject.time;
        }

        public static int GetVelocityForObject(HitObject hitObject)
        {
            switch (hitObject.hitsound)
            {
                case 0:
                    return 20;
                case 2:
                    return 60;
                case 3:
                    return 127;

                default:
                    return 20;
            }
        }

        public struct AudicaDataPos
        {
            public Cue.GridOffset offset;
            public int pitch;

            public AudicaDataPos(float offsetX, float offsetY, int pitch)
            {
                this.offset.x = offsetX;
                this.offset.y = offsetY;
                this.pitch = pitch;
            }
        }

        public struct Coordinate2D
        {
            public float x;
            public float y;

            public Coordinate2D(float x, float y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public static AudicaDataPos GetAudicaPosFromHitObject(HitObject hitObject)
        {
            float tempPosx = (hitObject.x / 512f * 8f - 4f) * Config.parameters.scaleX + 5.5f;
            float tempPosy = ((1 - hitObject.y / 384f) * 6f - 3f) * Config.parameters.scaleY + 3f;

            var x = Math.Clamp((int)(tempPosx), 0, 11);
            var y = Math.Clamp((int)(tempPosy), 0, 6);
            int pitch = x + 12 * y;

            float offsetX = (tempPosx - x);
            float offsetY = (tempPosy - y);

            return new AudicaDataPos(offsetX, offsetY, pitch);
        }

        public static AudicaDataPos CoordinateToAudicaPos(Coordinate2D coordinate)
        {
            float tempPosX = coordinate.x;
            float tempPosY = coordinate.y;

            int x = Math.Clamp((int)(tempPosX), 0, 11);
            int y = Math.Clamp((int)(tempPosY), 0, 6);
            int pitch = x + 12 * y;

            float offsetX = (tempPosX - x);
            float offsetY = (tempPosY - y);

            return new AudicaDataPos(offsetX, offsetY, pitch);
        }

        public static Coordinate2D AudicaPosToCoordinate(AudicaDataPos audicaPos)
        {
            float x = audicaPos.pitch % 12;
            float y = audicaPos.pitch / 12;
            x += audicaPos.offset.x;
            y += audicaPos.offset.y;

            return new Coordinate2D(x, y);
        }

        public static Coordinate2D GetPosFromCue(Cue cue)
        {
            AudicaDataPos audicaPos = new AudicaDataPos(cue.gridOffset.x, cue.gridOffset.y, cue.pitch);
            return AudicaPosToCoordinate(audicaPos);
        }

        public static bool CuesPosEquals(Cue cue1, Cue cue2)
        {
            return cue1.pitch == cue2.pitch && cue1.gridOffset.Equals(cue2.gridOffset);
        }

        public static float EuclideanDistance(float xFrom, float yFrom, float xTo, float yTo)
        {
            return (float)Math.Sqrt(Math.Pow(xTo - xFrom, 2) + Math.Pow(yTo - yFrom, 2));
        }

        private static float GetZOffsetForX(float x)
        {
            if (x < 0f) x *= -1f;

            if (x < 5.5f) return 0f;

            var zOffset = Math.Clamp(Math.Abs(x) - 5.5f, 0f, 2.5f) / 5f;

            return zOffset;
        }
    }
}
