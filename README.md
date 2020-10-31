
![Banner](https://i.imgur.com/TLKSbJc.png "Banner")

<p align="center">
<b>A highly customizable tool for converting maps from other rhythm games into Audica maps.</b><br>

While Audica does have a decent selection of well made custom songs available, this selection pales in comparison to some of the bigger rhythm games in the world, such as osu!, which has a selection of more than **a million** custom songs. osu! features gameplay and mapping that are similar to Audica in nature, creating an excellent opportunity to convert osu maps into Audica maps. Doing so makes the entire library of osu! custom maps, which is likely to contain any more well known song you can think of, available to Audica players. There are however also many fundemental differences between osu! and Audica gameplay, which makes this conversion task far from trivial. This conversion tool enables conversion of osu! maps into Audica, performing a lot of processing of the map to ensure high quality, playable Audica maps.

The gratest innovation from the previous attempts at an osu! converter, is a highly advanced hand assignment algorithm. Additionally, the converter features better scaling, including an adaptive scaling system, streams being converted to chains, long sliders converted to sustains, some targets being selectively converted into melees, stacked notes being distributed for readability, and proper timing conversion with support for variable bpm.
</p>

## Supported Games
Currently only converting maps from osu! is supported. While maps from any of osu!'s four game modes can be converted, only osu!standard maps make for decent, playable Audica maps.

## Instructions
* Download the latest release from [here](https://github.com/octoberU/AudicaConverter/releases).
* Drag-and-drop one or multiple `.osz` files onto the `AudicaConverter` exe within file explorer. You can also drag-and-drop folders of `.osz` files.
* Follow the directions in the console.

You can also associate the `.osz` file ending with the AudicaConverter exe, allowing you to convert maps by simply double-clicking the `.osz`file. To do so, right-click a `.osz` file -> "Open with...". Check the "Always use this app to open .osz files" checkbox, click "More apps" then "look for another app on this PC", navigate to and select the AudicaConverter exe.

osu! beatmaps can be downloaded as `.osz` files from the [official osu! website](https://osu.ppy.sh/beatmapsets?m=0) (requires login), or from the alternative, community hosted service [bloodcat](https://bloodcat.com/osu/).

**Note!** The difficulty of the resulting Audica map mainly depends on the difficulty of the osu! map you choose to convert, and can vary from very easy to inhumanly difficult. The converter allows you to choose between the different difficulties available for each osu! map, and will give you a numeric estimate of the difficulty of the resulting Audica map on conversion. For a point of reference with the Audica OST's, beginner ≈ 2, standard ≈ 3, advanced ≈ 4, expert ≈ 5 Audica difficulty rating+. You can help finding osu! maps that will convert to appopriate difficulties by filtering on osu! star difficulty rating. For example by adding "star>3 star<4" to your search on either the osu! official website or Bloodcat, you will only get maps that have a least one difficulty with a star rating between 3 and 4. Keep in mind that osu! star difficulty does not translate one-to-one to Audica difficulty rating, so it might take some trial and error to figure out what difficulties suit your preferences and skill level.

For efficient mass conversion of a large number of maps, the converter can be put into an auto conversion mode. See [this wiki page](https://github.com/octoberU/AudicaConverter/wiki/Auto-Conversion-Mode-Configuration-and-Operation) for details on setting up and running auto mode.

## Customization
The converter is highly customizable, with **a lot** of options that can be configured to adjust the converted maps to your preference. While the default configurations are optimized to provoide good conversions across a wide range of map styles, difficulties and player skill levels, a one-size-fits-all configuration isn't feasible. It is recommended to adjust settings such as the map size scaling and at what speed targets should be converted to chains based on your own preferences and experiences with the converter. See the [Converter Customization and Config Options](https://github.com/octoberU/AudicaConverter/wiki/Converter-Customization-and-Config-Options) wiki page for details on each option in the `config.json`.

## Sharing Converts
While the Audica converter is generally able to make decent Audica maps out of most osu! maps, the nature of the original map being made for a different game means that how well the resulting convert works as an Audica map might vary significantly from map to map. In order to help each other finding the osu! maps that makes for truely good Audica converts, we encourage you to share converts that stand out as particularly enjoyable with other players in the #converted-maps channel of the Audica Modding Group [Discord Server](https://discord.gg/cakQUt5).

## Feedback, Suggestions and Issue Reporting
The AudicaConverter will see continued development after initial release, in order to further improve the conversion results. Any feedback and suggestions on the converter, including simply pointing to maps with poor conversion results, would be helpful in further improvements to the converter, and greatly appreciated. Feedback and suggestions, in addition to reports of any technical issues that might arise using the converter, can be reported as an issue on this github repo's [issues](https://github.com/octoberU/AudicaConverter/issues) page, or in the #converter-feedback channel in the Audica Modding Group [Discord Server](https://discord.gg/cakQUt5).

## Internal Workings
The converter contains a lot of different, carefully designed systems for modifying and adapting osu! maps into more playable Audica maps. The most important and advanced of these are the hand assignment algorithm, which combines search/simulation techniques with mathematical modelling of strain to decide what hand each target should be required to be shot with. A more in-depth, technical description of the hand assignment algorithm can be found on [this wiki page](https://github.com/octoberU/AudicaConverter/wiki/Hand-Selection-Algorithm:-How-It-Works).
