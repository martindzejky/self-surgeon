# Exporting from Aseprite

1. Move all parts of the image to a separate layer
2. Mark all layers you want to export as visible
3. Open the target folder in a terminal
4. `/Applications/Aseprite.app/Contents/MacOS/aseprite --batch --split-layers {FILE}.aseprite --save-as {FILE}.png`
5. All layers will be exported as separate files

[Source](https://www.aseprite.org/docs/cli/#split-layers)
