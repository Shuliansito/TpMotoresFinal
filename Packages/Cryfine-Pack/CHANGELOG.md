## [0.0.3] - 2026-10-19

### Added
- `UtilityCF` class with Unity helpers:
  - `GetCenter(Transform)`: returns the center of an object's Renderer (searched in its children), or its position if it has none
  - `GetCenter(Renderer, Transform)`: same, using a Renderer that was already found (cheaper to call every frame)
  - `DrawLabel`: draws a text label at a world position in the Scene view (Editor only)
  - `DrawRangeFlat`: draws a flat circle on the ground to show a range (Editor only)
  - `DrawRangeSphere`: draws a wire or solid sphere to show a range (call it from `OnDrawGizmos`)


  ## [0.0.2] - 2026-10-19

### Added
- `DebugCF` class with debug helpers:
  - `Distance2GameObjects`: logs the Manhattan distance between two GameObjects
  - `IsAtDistance`: logs whether two GameObjects are within a given distance of each other
  - `DrawDistance`: draws a line between two points in the Scene view
  - `DrawDistanceLabel`: draws a line between two points with its distance as a label (Editor only)

## [0.0.1] - 2026-10-19

### Added
- Distance functions in `MathC`:
  - `DisMan2D`, `DisMan3D`: Manhattan distance between two points
  - `DisSqr2D`, `DisSqr3D`: squared Euclidean distance (no square root, faster for comparing distances)
  - `DisEuc2D`, `DisEuc3D`: Euclidean (straight-line) distance
  - `DisCheb2D`, `DisCheb3D`: Chebyshev distance (largest difference on any axis)
- Value functions in `MathC`:
  - `Clamp`: restricts a value between a minimum and a maximum
  - `Clamp01`: restricts a value between 0 and 1
  - `Lerp`: linear interpolation between two values, with `t` clamped to 0-1
  - `SmootherStep`: smooth 0-1 curve that eases in and out
  - `ExpDecay`: frame-rate independent smoothing of a value toward a target
  - `Wrap`: wraps an integer into the range [min, max)
