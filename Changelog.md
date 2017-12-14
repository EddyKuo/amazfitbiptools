# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).

## [Unreleased]

## [1.0.2.3] - 2017-12-10
### Changed
- Fixed packing .res files which was unpacked with latest version (using leading zeroes in image file names).

## [1.0.2.2] - 2017-12-09
### Added
- Used string representation of `"Alignment"` values. Possible text values are: `"TopLeft"`, `"TopCenter"`, `"TopRight"`, `"CenterLeft"`, `"Center"`, `"CenterRight"`, `"BottomLeft"`, `"BottomCenter"`, `"BottomRight"` (#7).
- Used JSON boolean values (`true` and `false`) for `"OnlyBorder"`, `"TwoDigitsMonth"`, `"TwoDigitsDay"` and `"AppendDegreesForBoth"`parameters.

### Changed
- Changed naming of extracted images to have 3 digits with leading zeroes (like 001.png instead of 1.png) (#9).
- Fixed displaying battery for 100% charged state for case when it was displayed by a set of images (#10).
- Fixed displaying of circle steps progress (#11).

## [1.0.2.1] - 2017-12-05
### Changed
- Fixed generating a preview for case when `Tens` or `Ones` are missing from `TwoDigits` block (#4)

## [1.0.2.0] - 2017-12-04
### Added
- Added generating animated previews.

### Changed
- Fixed drawing clock hands. They were skewed before.
- Improved speed of decoding/encoding images.

## [1.0.1.0] - 2017-12-02
### Added
- Generating preview on packing/unpacking watchface .

## [1.0.0.3] - 2017-11-29
### Added
- Added saving resources version on unpacking .res file.
- Added packing .res file (#1)

## [1.0.0.2] - 2017-11-28
### Added
- Added applying dithering for images on packing (#2)
- Added deleting incomplete bin if an error happened on packing.

## [1.0.0.1] - 2017-11-26
### Added
- Added ability to skip Off image for bluetooth and ability to add Off images for Lock, DND and Alarm elements.
- Added Net Framework 4.0 compatibility.

## [1.0.0.0] - 2017-11-26
### Added
- Implemented watchfaces unpacking and packing.
- Implemented .res file unpacking.

[Unreleased]: https://bitbucket.org/valeronm/amazfitbiptools/branches/compare/HEAD..1.0.2.3
[1.0.2.3]: https://bitbucket.org/valeronm/amazfitbiptools/branches/compare/1.0.2.3..1.0.2.2
[1.0.2.2]: https://bitbucket.org/valeronm/amazfitbiptools/branches/compare/1.0.2.2..1.0.2.1
[1.0.2.1]: https://bitbucket.org/valeronm/amazfitbiptools/branches/compare/1.0.2.1..1.0.2.0
[1.0.2.0]: https://bitbucket.org/valeronm/amazfitbiptools/branches/compare/1.0.2.0..1.0.1.0
[1.0.1.0]: https://bitbucket.org/valeronm/amazfitbiptools/branches/compare/1.0.1.0..1.0.0.3
[1.0.0.3]: https://bitbucket.org/valeronm/amazfitbiptools/branches/compare/1.0.0.3..1.0.0.2
[1.0.0.2]: https://bitbucket.org/valeronm/amazfitbiptools/branches/compare/1.0.0.2..1.0.0.1
[1.0.0.1]: https://bitbucket.org/valeronm/amazfitbiptools/branches/compare/1.0.0.1..1.0.0.0