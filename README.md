# Brain Games

## Setup and Installation

1. Sign up for GitHub Student Developer Pack at https://education.github.com/pack

1. Create a Unity account at https://id.unity.com

1. Download and install the Unity Student Pack (includes Unity, Unity Hub, and Unity Bug Reporter) at https://store.unity.com/#plans-individual

1. Launch Unity Hub:

    1. Navigate to the "Installs" page and install Unity Version 2019.4.0f1

    1. Navigate to the "Projects" page and "Add" brain-games/Brain to your "Projects" list

    1. Set the "Unity Version" of the project to "2019.4.0f1"

    1. Set the "Target Platform" of the project to "Current Platform"

    1. Click on "Brain" in the "Projects" list to launch Unity and begin developing

## Building, Testing, and Deploying

1. To build, deploy, and test the Unity project, we will be using Unity Cloud Build

1. Each time a change is pushed to the repository, a build will begin.

1. An email notification will be sent when the build is complete.

1. A Slack notification will be sent to the #brain-games channel when the build begins and completes.

1. To manually build and test:

    1. Visit your Unity dashboard at https://dashboard.unity3d.com/

    1. Using the Menu, navigate to the DevOps/Cloud Build/Projects
        
    1. Select project "Brain" to view the build history and start new builds

    1. To start a new build, click "Build: brain-games-mac-build" 

    1. To view a particular build's details, click "View details"

    ### Latest Build: SUCCESS

        1. [Unity] DisplayProgressbar: Unity license
        2. [Unity] Initialize engine version: 2019.4.0f1 (b487b79891e1)
        3. [Unity] A meta data file (.meta) exists but its asset 'Assets/Fire/Firebase/Plugins/x86_64/FirebaseCppApp-7_1_0.so' can't be found. When moving or deleting files outside of Unity, please ensure that the corresponding .meta file is moved or deleted along with it. 
        4. [Unity] A meta data file (.meta) exists but its asset 'Assets/Fire/Firebase/Plugins/x86_64/FirebaseCppAuth.so' can't be found. When moving or deleting files outside of Unity, please ensure that the corresponding .meta file is moved or deleted along with it. 
        5. [Unity] A meta data file (.meta) exists but its asset 'Assets/Fire/Firebase/Plugins/x86_64/FirebaseCppDatabase.so' can't be found. When moving or deleting files outside of Unity, please ensure that the corresponding .meta file is moved or deleted along with it. 
        6. [Unity] Assets/Scripts/FastMath.cs(12,13): warning CS8321: The local function 'questionGenerator' is declared but never used
        7. [Unity] Assets/Scripts/FastMath.cs(50,14): warning CS8321: The local function 'randomAnswerGenerator' is declared but never used
        8. [Unity] UnityEngine.Debug:LogWarning(Object)
        9. [Unity] Request timed out while processing request "https://public-cdn.cloud.unity3d.com/config/production", HTTP error code 0
        10. UPM Server already running, skipping...
        11. [Unity] DisplayProgressbar: Unity license
        12. [Unity] Initialize engine version: 2019.4.0f1 (b487b79891e1)
        13. [Unity] UnityEngine.Debug:LogWarning(Object)
        14. UPM Server already running, skipping...
        15. [Unity] DisplayProgressbar: Unity license
        16. [Unity] Initialize engine version: 2019.4.0f1 (b487b79891e1)
        17. [Unity] UnityEngine.Debug:LogWarning(Object)
        18. [Unity] Assets/Scripts/FastMath.cs(12,13): warning CS8321: The local function 'questionGenerator' is declared but never used
        19. [Unity] Assets/Scripts/FastMath.cs(50,14): warning CS8321: The local function 'randomAnswerGenerator' is declared but never used
        20. [Unity] Finished exporting player successfully.
        21. publishing finished successfully.
        22. Finished: SUCCESS

1. To deploy:

    1. To deploy a particular build, click ⋮ next to "View Details"

    1. Select "Share Link"

    1. Select "Open link in the new window"

    1. In the new window, select "Download .ZIP File"

    1. Unzip the file and open the folder
    
    1. Launch "basics copy"

    1. Play the game

    ### Latest Deployment

        1. https://developer.cloud.unity3d.com/share/share.html?shareId=b1lFhi6NgP

## Project Goal & Description

Goal: to assist students with developing and strengthening their cognitive abilities through playing mini-games that challenge and improve various cognitive skills.

The mini games are classified according to cognitive skill:

1. **Memory**: strengthens short-term memory and increases the ability focus.
    - Match the Cards: the player is presented with an even amount of cards that are face down. 
    Each round, the player is allowed to flip a pair of cards up to find a match. If the pair of cards do not match, they are flipped back to their face-down position. If the pair of cards match, they remain face-up. The player continues to select pairs of cards until all of the matches are found. The player must complete this task within a certain allotment of time.

1. **Language**: enhances the user's fluency in and articulation of the English language.
    - Word Permutations: the player is presented with a set of letters. Within a certain allotment of time, the player must construct as many word permutations as they can, using the letters provided.

1. **Perception**: enhances the user's ability to process information quicker and improves visual feedback reflexes.
    - Spot the Difference: the player is presented with two images. The image on the right is a manipulation of the image to the left. Within a certain allotment of time, the player must use the original image to the left to find the modifications in the image to the right.

1. **Numeracy**: enhances the user's algebraic skills.
    - Fast Math: the player is presented with a series of two-operand ( + , - , * , / ) math problems. A new question is presented after the previous question has been answered. The player must answer as many problems as they can within a certain allotment of time.

1. **Reasoning**: enhances the user's ability to recognize patterns and identify relationships between objects.
    - Tangrams: the player is provided with a number of shapes and an outline. The player must orient the shapes correctly within the outline. To properly solve the puzzle, there must be no shapes that overlap or go outside of the outline. There also cannot be any empty spaces inside of the outline.

## Functional Requirements

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

1. The user can view their progress meter after each mini-game completion.
    - Displays the percentage of mini-game badges earned out of total mini-game badges for that category

1. The user can opt-in to (standard) or opt-out of appearing on a public level leaderboard.

1. The user can view their profile which displays their statistics.
    
    - Level and level name
        - Level is based on the total amount of points earned
        - Each level increment requires a different amount of points
        - Level names: Novice, [...], Intermediate, [...], Master
        - Level names account for a certain domain of levels

    - General game badges
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

    - High score per game category (max of all mini-game high scores)
        - High score per mini-game

    - Total points accumulated across all categories
        - Total points accumulated per category

    - Number of mini-games played within a category

## Design Implementaion

1.  We will use Firebase Database REST API.

