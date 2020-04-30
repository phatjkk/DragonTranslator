Capture2Text Readme
--------------------------------------------------------------------------------

Capture2Text enables users to quickly OCR a portion of the screen using a
keyboard shortcut.

For more information visit:
http://capture2text.sourceforge.net/

--------------------------------------------------------------------------------
Version History:
--------------------------------------------------------------------------------
[Version 4.6.2 (8-10-2019)]
- Ticket #49, #72: Fix error when using CLI to OCR 8 bpp images.
- Ticket #76: Fix "\u200C" character being added when replacing ligatures.
- Ticket #68: Fix typo in About dialog.
- Fix typo: "Keep lines breaks" -> "Keep line breaks".

[Version 4.6.1 (7-3-2019)]
- Ticket #97: Fixed issue where hex characters were appended to the translation.

[Version 4.6.0 (4-21-2018)]
- Ticket #48: \t, \r, and \n can now be used in the "Replace with" column.
- Ticket #43: Replace nuisance ligatures (ﬁ, ﬂ).
- Ticket #30: Non-32bpp images now supported in CLI. Note: 1bpp images will not
  be pre-processed.
- Added "Settings > Output > Call Executable" option.
- In Settings dialog, show tab menu as a list box.

[Version 4.5.1 (11-4-2017)]
- Ticket #27: Fixed text-to-speech feature not working due to missing
  qtexttospeech_sapi.dll.
- Fixed bug that caused some save data to be stored in the registry.

[Version 4.5.0 (10-22-2017)]
- Ticket #26: Added text-to-speech feature.
- Ticket #23: Added "scale factor" option to CLI and Settings dialog.
- Ticket #21: Fixed occasional column merge issue for Japanese vertical text.
- Update to Tesseract 4.00alpha (Note: Capture2Text will continue to be packaged
  with legacy traineddata until newer LSTM fast/best traineddata is more mature)
- Update to QT 5.9.2 and Leptonica 1.74.4.

[Version 4.4.0 (7-28-2017)]
- Ticket #16: Fixed issue where only first line of multi-line capture was translated.
- Ticket #14: Added CLI option --clipboard.
- Ticket #13: You may now call Capture2Text.exe with the --portable option to
  place the .ini settings file in the same directory as the .exe.
- Ticket #12: Added "Trim capture" option to the Setting dialog.
- Ticket #12: Added CLI option --trim-capture.
- Added CLI option --deskew.

[Version 4.3.0 (6-2-2017)]
- Ticket #6: For CLI, output after each file is processed instead of outputting
  after all files have been processed.
- Ticket #6: Added new CLI --output-format token: ${file}.
- Ticket #5: Added CLI option --debug-timestamp.

[Version 4.2.0 (5-13-2017)]
- Ticket #4: Added option to log captures to file.
- Ticket #4: Added option to append timestamp to debug images.
- Ticket #4: Added CLI options --output-file-append and --output-format.

[Version 4.1.0 (4-14-2017)]
- Ticket #2: Fixed bug that caused CLI option "--screen-rect" to output an error.
- Ticket #1: Added hotkey to toggle whitelist on/off. By default this hotkey is unmapped.
- Ticket #1: Added hotkey to toggle blacklist on/off. By default this hotkey is unmapped.
- Ticket #1: Added option to specify a Tesseract config file to both GUI and CLI.
- Added whitelist and blacklist options to CLI.
- Increased default lengths for text line captures.
- Show help text when no options are provided to Capture2Text_CLI.exe.
- Added suffix to some of spin boxes in the settings dialog.
- Reduced border width in popup dialog.
- Add version number to the .ini file.

[Version 4.0 (4-2-2017)]
- Complete re-implementation in QT/C++.
- Added Translation feature (powered by Google Translate).
- Added Re-Capture Last hotkey.
- Added Text Line Capture hotkey.
- Added Forward Text Line Capture hotkey.
- Added Bubble Capture hotkey.
- Added more Preview position options.
- Added blacklist setting.
- Added "Reset to defaults" links in Settings dialog.
- Capture Box and Preview Box may now have outlines.
- Better interface for specifying hotkeys in the Settings dialog.
- Custom tray icon "balloon" window.
- Added "Replace" tab to the Settings dialog. Substitutions/Replacements
  are now stored in the settings .ini instead of substitutions.txt.
- Added sample Capture Box to Settings dialog.
- Added sample Preview box to Settings dialog.
- Added deskew option.
- Added debug options.
- Popup dialog now enabled by default.
- Size of Popup dialog is now saved automatically.
- Added "Topmost" option to Popup dialog.
- Added "Font" option to Popup dialog.
- Removed the "Enable OCR pre-processing" option (now always enabled).
- Removed the "Strip furigana" option (now always enabled).
- Removed the "OCR method" option.
- Removed "Prepended/Appended Text" setting.
- Removed "Send to Cursor" setting.
- Removed "Send to Control" setting.
- "Preserve newline characters" setting renamed to "Keep linebreaks".
- "Preferences" dialog renamed to "Settings".
- Added to Capture2Text_CLI.exe for command line usage.
- Settings .ini file now stored in %appdata%\Capture2Text.
- Changed some of the hotkey defaults.
- Added Russian and Korean to default package and removed Italian.
- Added icons to some of the items in the tray menu.
- Added more information in the About dialog.

[Version 3.9 (6-5-2016)]
- Updated active selection corner logic. (Thanks R. Webster-Noble!).

[Version 3.8 (1-15-2016)]
- Updated Tesseract (3.05.00dev).
- Support for additional languages.
- Added the "OCR Method" setting.
- NHocr is no longer packaged (but may still be copied from previous versions
  to the Utils folder)

[Version 3.7 (7-04-2015)]
- Text entered into the popup window will now be saved to the clipboard when the
  OK button is clicked and the Save to Clipboard option is checked.

[Version 3.6 (5-15-2015)]
- Removed the experimental speech recognition feature due to new Google
  Speech API v2 quota restrictions.
- Fixed DPI scale issue with the capture box. (Thanks rocker7!).
- Now compiled with AutoHotkey 32-bit Unicode v1.1.22.00 (was v1.1.14.03).

[Version 3.5 (7-17-2014)]
- Capture box should be less jumpy.
- Preview will now only update when the user has stopped moving the capture box
  for at least 400 milliseconds.
- When preview is setting to "Dynamic", the positioning should be less jumpy.

[Version 3.4 (7-10-2014)]
- Added option to strip furigana from Japanese text.
- Added the "Auto" choice to the "Text direction" preference.
- Removed the option to toggle "OCR pre-processing" from the Preferences. It
  may still be edited in settings.ini.
- Changed the default "OCR pre-processing" hotkey to Shift-Ctrl-Windows-B.

[Version 3.3 (3-2-2014)]
- More minor tweaks to the Preferences dialog.

[Version 3.2 (3-1-2014)]
- Minor tweaks to the Preferences dialog.

[Version 3.1 (2-28-2014)]
- Improved OCR accuracy through use of better image pre-preprocessing (leptonica_util).
- Now supports text and backgrounds of any color when OCR pre-processing is enabled.
  (In the previous version, only dark text on a light background was supported).
- Added option to place the preview text beside the capture box.
- Japanese (Tesseract) accuracy is now vastly improved through use of a Japanese-specific
  Tesseract config file. Also using this config file with Chinese (Tesseract).
- Using Tesseract v3.02.02 for Japanese (was v3.01).
- Replaced the binarize option with the OCR pre-processing option.
- Removed "Send to Control" from the right-click menu.
- Removed the Chinese (NHocr) language pack from default distribution. (You can
  still download it from https://code.google.com/p/nhocr/downloads/list).
- Added the Italian language pack to the default distribution.
- Removed setting of PreviewRemoveCaptureBox from the GUI.
- Removed ConvertImageFormat (replaced with leptonica_util).
- Now compiled with AutoHotkey 32-bit Unicode v1.1.14.03 (was v1.1.11.01).

[Version 3.0 (8-27-2013)]
- Added option to binarize captured image before sending it to the OCR engine.

[Version 2.5 (7-5-2013)]
- Updated NHocr from v0.20 to v0.21.
- Now compiled with Ahk2Exe v1.1.11.01 instead of v1.1.05.06.

[Version 2.4 (12-29-2012)]
- Added support for Arabic, Danish (Alternate), Esperanto (Alternate),
  German (Alternate) and Slovakian (Alternate).

[Version 2.3 (11-9-2012)]
- Added option to remove the capture box before a preview OCR. This is more
  accurate, particularly with NHocr, but causes the capture box to flicker.
- Changed the default image scale factor from 300% to 320% to meet Tesseract's
  minimum recommended DPI.
- When using Japanese, revert to Tesseract v3.01. It is MUCH more accurate than v3.02.02.
- Now passing a .ppm image to NHocr instead of a .pgm image to better handle
  non-grayscale captures.
- Increased update rate of the capture box to make it appear more fluid.
- Fixed text direction being ignored bug for Chinese/Japanese that was introduced in v2.2.
- Fixed bug that caused the capture box to stick around after it was supposed to
  be removed.

[Version 2.2 (11-4-2012)]
- Upgraded to Tesseract v3.02.02. For details, see:
  http://code.google.com/p/tesseract-ocr/wiki/ReleaseNotes
- Added whitelist option to the OCR tab.
- Simplified substitution tokens and fixed whitespace bug.

[Version 2.1 (10-7-2012)]
- Added the substitutions feature.
- Added command line options.

[Version 2.0 (3-10-2012)]
- Added the Preferences dialog. No more editing settings.ini by hand.
- The popup window is now multi-lined.
- Added option to preserve newline characters.
- Limited preview to 150 characters. A trailing "..." will appear if necessary.
- Added Speech Recognition Language option to right-click menu.
- Cleaned up the right-click menu.
- On the first run, inform user how to access the Preferences dialog.

[Version 1.10a (2-18-2012)]
- Removed GdiPlus.dll from distribution.

[Version 1.10 (12-31-2011)]
- Added preview box (and corresponding settings)

[Version 1.09 (11-10-2011)]
- Fixed speech recording stopping in the middle of a sentence.
- Fixed VoiceMaxResults not working correctly. Also increased to 9 as default.

[Version 1.08 (11-06-2011)]
- Upgraded Tesseract to version 3.01 (it has better vertical text support and
  doesn't ignore small captures as much)
- When using Tesseract Chinese or Japanese, you can now select the text
  direction (vertical or horizontal). To support this, added
  TextDirectionToggleKey and textDirection to settings.ini.
- Changed default for ScaleFactor from 4.0 to 3.0 in settings.ini.
- Changed menu text for Chinese and Japanese to reflect the OCR engine being used.

[Version 1.07 (11-05-2011)]
- Added voice recognition support via unofficial Google voice recognition service
- Added the "Send To Cursor" option to menu. The setting.ini file includes:
    SendToCursor
    SendToCursorApplyBeforeAndAfterCommands
- Renamed OCRAdjustment to OCRSpecific in settings.ini
- Moved the CaptureBox section in settings.ini to the OCRSpecific section
- Added VoiceSpecific to settings.ini. Section includes:
    VoiceMaxResults
    VoiceResultsWindowWidth
    VoiceResultsWindowFont
    VoiceResultsWindowFontSize
    VoiceSilenceBeforeStop
    VoiceLanguage
- Added StartVoiceCapture to Hotkey section in settings.ini
- Added VoiceLanguageToggleKey to Hotkey section in settings.ini
- Removed scaleFilter from settings.ini
- Removed the scaleFactor option from the menu (it's still in settings.ini)

[Version 1.06 (12-12-2010)]
- Added language quick access keys.
- For Chinese and Japanese delete newlines. For other languages replace
  newlines with spaces.

[Version 1.05 (12-04-2010)]
- Fixed issue where the checkboxes in the language menu wouldn't disappear.

[Version 1.04 (12-04-2010)]
- Added ability to move the capture box by right-clicking
- Added languages supported by the Tesseract OCR tool
- Created a right-click menu that allow the user to select language, output type,
  capture box settings and scale factor
- Removed unnecessary items from settings.ini

[Version 1.03 (11-27-2010)]
- Added ability to change dictionary when the Dictionary setting in settings.ini
- Added Chinese dictionary

[Version 1.02 (11-27-2010)]
- Changed CaptureKey to StartAndEndCaptureKey in settings.ini
- Added EndOnlyCaptureKey to settings.ini
- Added ToggleActiveCaptureCornerKey to setting.ini

[Version 1.01 (11-27-2010)]
- Added ReplaceControlText to settings.ini
- Added ability to use linefeeds, carriage returns and tabs in PrependText and AppendText
- Added an "About" item to the tray menu.
- Removed the capture box showing up in the taskbar
- Removed the PassThruKey settings in settings.ini. They are no longer needed.
- Changed the tray tooltip text
- Cleaned up code and put the ScreenCapture routines in a separate file

[Version 1.00 (11-26-2010)]
- Initial version

--------------------------------------------------------------------------------

