<img src="Design/banner.jpg" style="margin-bottom:10px" />

# Meadow.Desktop.Samples

Public project samples for Meadow.Windows and Meadow.Linux. Click on any of the projects to open the source code and run it.

## Contents
* [Purchasing or Building](#purchasing-or-building)
* [Getting Started](#getting-started)
* [Hardware Specifications](#hardware-specifications)


## Windows

To run these samples, make sure:
1. Your development environment is [properly configured](http://developer.wildernesslabs.co/Meadow/Getting_Started/Getting_Started_Meadow.Desktop/Getting_Started_Windows/) to run Meadow apps on Windows.
2. The sample you wish to run, add the native library (libmpsse.dll) of the FT232H IO Expander depending on your CPU's architecture ([Win32 or x64](Native/Windows/)) to that project and set the **Copy to Output Directory** to `Copy if newer` or `Copy always`

![Copy to Output Directory for libmpsse.dll file](Design/build-action.png)

3. Rebuild and right-click the project and click on **Set as Startup Project** 
4. Click **Debug** to run see a Meadow App running on Windows!

### Meadow.Windows Samples
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
