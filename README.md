<img src="Design/banner.jpg" style="margin-bottom:10px" />

# Meadow.Desktop.Samples

Public project samples for Meadow.Windows and Meadow.Linux. Click on any of the projects to open the source code and run it.

## Contents
- [Windows](#windows)
    - Pre-requisites
    - [Meadow Windows Samples](#meadow-windows-samples)
- [Linux](#linux)
    - [Meadow Linux Samples](#meadow-linux-samples)
- [Support](#support)

## Windows

### Pre-Requisites

1. Your Windows machine is [properly configured](http://developer.wildernesslabs.co/Meadow/Getting_Started/Getting_Started_Meadow.Desktop/Getting_Started_Windows/) to run Meadow apps projects.
2. To run a sample that uses an IOExpander like the FT232H, you must add the native library (libmpsse.dll) of the IO Expander that corresponds on your CPU's architecture ([Win32 or x64](Native/Windows/)) to that project and set the **Copy to Output Directory** to `Copy if newer` or `Copy always` as shown below:

<p align="center">
    <img src="Design/build-action.png" style="width:50%" />
</p>

3. Rebuild and right-click the project and click on **Set as Startup Project** 
4. Click **Debug** when ready to run see a Meadow App running on Windows!

### Meadow Windows Samples

<table>
    <tr>
        <td>
            <img src="Design/blinky.png"/><br/>
            Control an RGB LED with an FT232H IO Expander</br>
            <a href="Source/Windows/Blinky/">Source Code</a>
        </td>
        <td>
            <img src="Design/character-display.png"/><br/>
            Using a Character Display with an FT232H IO Expander</br>
            <a href="Source/Windows/CharacterDisplaySample/">Source Code</a>
        </td>
        <td>
            <img src="Design/micrographics-intro.png"/><br/>
            Show weather data on a display with an FT232H IO Expander</br>
            <a href="Source/Windows/WifiWeather/">Source Code</a>
        </td>
    </tr>
    <tr>
        <td>
            <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
        </td>
        <td>
            <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
        </td>
        <td>
            <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
        </td>
    </tr>
</table>

## Linux

Instructions and samples coming soon...

### Meadow Linux Samples

<table>
    <tr>
        <td>
            <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
        </td>
        <td>
            <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
        </td>
        <td>
            <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
        </td>
    </tr>
</table>

## Support