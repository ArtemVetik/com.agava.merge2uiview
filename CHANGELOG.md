# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.0.0] - 2023-04-20

### Added

- `OpeningDelayRepositoryFactory` for setting up opening delay of items.
- `OpeningDelayCommandFactory` to configure the opening delay command.
- `OpeningDelayView` to display the item unlock button.
- `ItemTrashCompositeRoot` and other components to configure the removal of items from the board. 

### Removed

- Remove abstract class `CooldownView`. Now you need to use `RepositoryTimersView` to display timers.

### Changed

- `MergeRoot` is divided into several CompositeRoots.

## [2.0.0] - 2023-04-14

### Added

- Add `CooldownRepositoryFactory` for setting up cooldown of items.
- Add `CooldownCommandFactory` to configure the cooldown command.
- Add an abstract class `CooldownView` to display the cooldown.
- Add fields to configure cooldown in `MergeRoot`.
- Add an example of cooldown of items in `Samples`.

### Changed

- In the `ClickCommand` setting, you can set the list of `ClickCommandFactory` assets.
- `com.yellowsquad.assetpath` updated to version `2.1.0`.

## [1.1.0] - 2023-04-11

### Added

- List of all ids that have a ClickCommand.
- Add the `RewardValueFactory` class to create an instance of the `IRewardValue` type.

### Changed

- Now the package requires a dependency on `com.agava.merge2` version `1.1.0`.
- `OpenedItemList` is now public.
- `ExampleTaskFactory` has been completely redesigned and automatically adds tasks to the list.
- Two fields have been added to the `TaskRoot` class of tasks to initialize task rewards.
- Add the `TaskReward` argument to the `TaskView.Init()` method.

### Fixed

- Year in LICENSE.md.

## [1.0.0] - 2023-04-04

Initial release.