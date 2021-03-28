# Brain Games

In order to build production, in the top level directory, run:

`make prod`

To make all proper installations, run:

'make dev_env'

## Project Goal & Description

Goal: to assist students with developing and strengthing their cognitive abilities through playing mini-games that challenge and improve various cognitive skills.

The mini games are classified according to cognitive skill:

1. **Memory**: strengthens short term-memory and increases the ability focus.
    - Match the Cards: the player is presented with an even amount of cards that are face down. 
    Each round, the player is allowed to flip a pair of cards up to find a match. If the pair of cards do not match, they are flipped back to their face-down position. If the pair of cards match, they remain face-up. The player continues to select pairs of cards until all of the matches are found. The player must complete this task within a certain allotment of time.

1. **Language**: enhances the user's fluency in and articulation of the English language.
    - Word Permutations: the player is presented with a set of letters. Within a certain allotment of time, the player must construct as many word permuations as they can, using the letters provided.

1. **Perception**: enhances the user's ability to process information quicker and improves visual feedback reflexes.
    - Spot the Difference: the player is presented with two images. The image on the the right is a manipulation of the image to the left. Within a certain allowment of time, the player must use the original image to the left to find the mofifications in the image to the right.

1. **Numeracy**: enhances the user's algebraic skills.
    - Fast Math: the player is presented with a series of two-operand ( + , - , * , / ) math problems. A new question is presented after the previous question has been answered. They player must answer as many problems as they can within a certain allotment of time.

1. **Reasoning**: enhances the user's ability to recognize patterns and identify relationships between objects.
    - Tangrams: the player is provided with a number of shapes and an outline. The player must orient the shapes correctly within the outline. To properly solve the puzzle, there must be no shapes that overlap or go outside of the outline. There also cannot be any empty spaces inside of the outline.

## Requirements

The requirements for the Game API server are:

1. The user can make a new account.

1. The user can delete their account.

1. The user can select a game category.

1. The user can select their current level or any level that they previously completed.

1. The user can start a new mini-game.

1. The user can exit the current mini-game.

1. The user can view their current mini-game progress as they play.
     - Time remaining
     - Current score
     - High score

1. The user can earn badges.

1. The user can view their prgress meter after each mini-game completion.
    - Displays the percentage of mini-game badges earned out of total mini-game badges for that category

1. The user can opt-in to (standard) or opt-out of appearing on a public level leaderboard.

1. The user can view their profile which displays their statistics.
    
    - Level and level name
        - Level is based on the total amount of points earned
        - Each level increment requires a different amount of points
        - Level names: Novice, [...], Intermediate, [...], Master
        - Level names account for a certain domain of levels

    - Overall game badges
        - Played a certain amount of mini-games within a category
        - Played a certain amount of mini-games over all categories
        - Earned all badges within a specific category
        - Earned all badges for all mini-games
        - Played "Brain Games" for a certain amount of hours

    - Mini-game badges
        - Word Permutations: found a secret word
        - Match the Cards: found an easter egg matching card pair
        - Fast Math: answered a certain amount of questions within the allotted time
        - General: finished a mini-game before timer ended

    - High score per catagory (max of all mini-game high scores)
        - High score per mini-game

    - Total points accumulated across all categories
        - Total points accumulated per category

    - Number of mini-games played within a category

## Design

Most of the above requirements will map directly to an API endpoint. We will use flask restx for our API server. We want the options available to the user stored on the server. This way, menus, etc. live in a single place.

Some design issues to be resolved:
1. How do we specify a game?
1. How much visual guidance resides on the server?
