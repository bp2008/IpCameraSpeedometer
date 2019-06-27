# IpCameraSpeedometer
Approximates the speed of moving objects using an IP camera.  

It is really clunky and not necessarily accurate, but perhaps fun to play with.  This app also needs additional effort before it will be robust.  Currently if the streaming connection is lost it probably won't be reconnected until the service is restarted!

## Requirements
* Windows 7 or newer, 64 bit, with [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472) installed.
* IP camera capable of streaming H.264 video via RTSP.

## Setup
1) Position the IP camera so its line of sight is [perpendicular](https://www.google.com/search?q=perpendicular) to the path of travel (such as a road).

2) **Download** IpCameraSpeedometer from the Releases tab.

3) **Extract** and **run** the executable.

4) Click **Configuration** and configure as desired.

5) Click "**Install Service**", then "**Run Service**".

## Example Output

This is a shapshot captured using Blue Iris to overlay the text, using this as a template `[%HMETER-20-96.5%] %MPH1% MPH`

![Example Output](https://i.imgur.com/e0bFhp4.jpg)
