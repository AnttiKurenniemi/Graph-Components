# Graph-Components

This repository contains a Bar Chart component for Winforms. This component started life as something quick 
and simple I needed for a project - everything I found online at that time was either too big and complex
(read: I was lazy and did not want to learn how to use them) or required registration or some such. So I did
what all smartypants-developers do: I made my own.

The component is super simple and not fancy by any means. It can only do bar charts for now, but I have some
variations that I'm working on and will probably publish at a later time, if I get them to work well enough.

The repository contains a sample app and the actual component. The component is a single file, "BarChartGraphic.cs",
which you just plonk on to your Winforms app and use. No packages, no installers, no nothing.

![Sample app](https://github.com/AnttiKurenniemi/Graph-Components/blob/master/Graphing%20Component%20Sample/Images/BarChart_SampleApp.png)

## Usage

Really simple, build your project once with the code included, and then add the values in code:

    barChart.AssignValue("First", 10);
    barChart.AssignValue("Second", 23);

The AssignValue method takes two parameters, key and value. Key will be displayed in the legend, so keep it
fairly short. If you add a new value with a key that already exists, only the value will be updated - no
duplicate keys will be added.

The chart does not auto-render! I did this on purpose, because when adding a bunch of stuff I didn't want it
to constantly repaint. In order to actually see your values, call Redraw method after you have added and/or
modified the values:

    barChart.Redraw();
    
If you want to highlight a bar, set the HighlightedValue to the key you want to highlight:

    barChar.HighlightedValue = "First";

Remember to call Redraw after setting the highlighted value. To remove highlighting, set it to empty:

    barChar.HighlightedValue = "";


## License

Do whatever you want with it.

## Test project

There are no tests at the moment; the test project is something I'm stuck with. I'd like to test the 
calculations of the drawing, but it's all private stuff and I don't know how much effort I want to 
put into surfacing it. Maybe later, gator.
