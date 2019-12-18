# ECS Gadgets

Various ECS stuff like [E7ECS](https://github.com/5argon/E7ECS) except that I am no longer a noob.

- `ECSTestBase` : Base class to inherit from for most ECS tests.
- `EntityManagerUtility` : Collection of one liner methods for better unit test code and sometimes could be used in production.
- `ConstantDeltaTimeSystem` : Replaces the official time system with one that always use the same delta time.

## Disclaimers

- I will add or remove any part of this package at my convenience and my needs. There is no SemVer, consider every update a breaking change.
- Some methods maybe committed even when I know it doesn't work fully yet. Thanks to habit of always `git add -A` ...
- You can submit bugs in the issue section, though I am not responsible for damages caused by the bugs!
