# 🌑 Noite Sem Lua: As Sombras da Promessa — Brazilian Folklore Serious Game 

A high-performance, single-player atmospheric serious game built on **Unity** and **C#** that translates Brazilian oral traditions and regional myths into an immersive, puzzle-solving gameplay experience. Designed as a Computer Science graduation thesis by Nicholas Schreen for the University of Fortaleza (UNIFOR) under the guidance of Prof. Dr. Belmondo Rodrigues Aragão Junior, the project evaluates how software architecture constraints, event-driven narrative triggers , and diegetic user interfaces can be leveraged to preserve regional cultural heritage against the challenges of rapid urbanization and globalization.

---

## 🎯 Project Overview & Academic Motivation

The core purpose of this project is to explore and evaluate the viability of **Serious Games** as modern vehicles for teaching, learning, and cultural preservation.With the steady decline of oral storytelling in local communities due to global culture pressures , *Noite Sem Lua* creates an interactive digital platform to keep these narratives alive for new generationsThe game places the player in an immersive environment to explore regional myths and legends.

Rather than relying on generic jump-scares, the game builds engagement through **psychological suspense, atmospheric puzzles, and environmental storytelling**.Players interact with iconic entities of Brazilian folklore, combining entertainment with the active preservation of Brazil's cultural traditions.

### The 4 Paradigm Pillars
1. **Diegetic Narrative Core (Dante's Journal & Newspapers):** Integration of task logs, objectives, and historical lore documents directly into the game-world to organize the story without breaking immersion.
2. **Tilemap Color-Tint Shifting:** Programmatic alteration of the 2D tilemap renderers to transition from day to night without the performance cost of dynamic lighting systems.
3. **Event-Driven Narrative Triggers (Trigger Colliders 2D):** A system of automatic and manual interaction prompts (using the "E" key) that drive story developments and map events.
4. **Integrated Audio Management:** An optimized 4-channel audio layout (Music, Ambient, UI, SFX) designed to build sensory immersion and tension.

---

## 🌐 Narrative & Gameplay Integration Matrix

To maintain historical accuracy, each folklore entity is represented through traditional regional archetypes transformed into narrative triggers and puzzle goals.

| Folklore Entity | Traditional Cultural Archetype | In-Game Narrative / Puzzle Mechanic | Serious Game Educational Objective |
| :--- | :--- | :--- | :--- |
| 🌪️ **Saci-Pererê** | Naughty boy, symbol of resistance, malice, and freedom. | Destroy key objects, requiring exploration; triggers characteristic whirlwind sounds. | Teaches adaptability, observation, and cultural recognition. |
| 👣 **Curupira** | Forest protector with backward feet to confuse hunters. | Confuses paths; requires players to leave a traditional offering in a hollow tree trunk. | Reinforces environmental respect and understanding of ancestral protective myths. |
| 🐊 **Cuca** | Fearful alligator-like figure used for social control and discipline. | Blocks progression near her cave; requires deciphering legends and local warnings. | Encourages logical deduction, text comprehension, and narrative analytical thinking. |
| 🪦 **Corpo-Seco** | Cursed corpse rejected by the earth, personifying moral punishment. | Blocks specific pathways; requires investigating historical records to bypass. | Teaches historical parables, moral imagery, and critical regional context. |

---

## 🛡️ Software Architecture & Atmospheric Optimization

[cite_start]To ensure smooth performance on low-spec computers (such as school laptops), *Noite Sem Lua* avoids demanding heavy graphics hardware routines by implementing optimized software patterns in Unity[cite: 45, 272].

### 1. Finite State Machine for Game Phases
The game loop is controlled by a centralized state machine that prevents script-contention and manages clean scene transitions. The active game flow state can be modeled conceptually across these core environments:

`Game_State = { MainMenu, DayExploration, DuskSuspense, JournalActive, GameOver }`

By restricting updates according to the active state, unnecessary coordinate checks and inputs are eliminated.

### 2. Procedural Tilemap Color-Tint Atmosphere
The distinction between day and night cycles is achieved directly and efficiently by programmatically adjusting the color tint property of the Unity Tilemap Renderers on the grid component:
* **Day Phase:** The grid applies standard natural tones to represent normal daylight exploration.
* **Night Phase:** The grid color property shifts programmatically to purplish and bluish tones to evoke the dark, tense atmosphere of a moonless night

This straightforward configuration provides a highly consistent visual identity with virtually zero extra rendering overhead, completely replacing complex or heavy lighting systems.

### 3. Integrated Audio Management (AudioManager)
The game utilizes a centralized 2D `AudioManager` containing **4 dedicated audio mixing channels** running sound assets from Pixabay:
* **Music (Música):** Runs continuous thematic background scoring.
* **Ambient (Ambiente):** Handles environmental noise loops like wind, rain, and forest murmurs.
* **UI (Interface):** Generates tactile audio feedback for inventory and menu inputs.
* **SFX (Efeitos Sonoros):** Controls short physical audio clips for gameplay interactions.

### 4. Diegetic Narrative Core: Dante's Journal & Newspapers
The plot unfolds through physically interactive game-world objects rather than intrusive UI menus:
* **Diário de Dante (Dante's Journal):** Acts as the primary bridge between narrative and control, organizing the plot by tracking specific active missions, such as *Trilho da Saudade* and *Reabastecimento do Engenho*.
* **Collectible Newspapers (*Informativo Regional - 1984*):** Scattered pieces that provide clues about entities (such as Curupira's offering or Cuca's cave mist) to expand the world context organically.

### 5. Asynchronous JSON Serialization
Player progress and journal updates are recorded dynamically. To prevent frame-rate drops during saving, data serialization runs automatically using the `ProgressionManager`:

`Save_On_Transition -> OnApplicationQuit() OR OnApplicationPause()` 

This provides clean persistence without the computational overhead of local databases.

---

## 📈 Academic Evaluation & Future Works

The validation framework for *Noite Sem Lua* is designed around its capabilities as an educational tool and an optimized software build.

* **Educational Testing:** Built to measure a player's cultural information retention delta via pre-game and post-game testing loops with elementary and high school students.
* **Internationalization:** Future development targets English and Spanish localized versions for international reach.
* **Expansion:** Plans to add new folklore entities from other regions of Brazil.
* **Academic Dissemination:** An academic paper detailing this cultural serious game approach has been accepted at **SBGAMES (Trilha Cultura)**.

---

## 📊 Framework Comparison & Cultural Inspiration

*Noite Sem Lua* adapts proven survival-horror and cultural narrative frameworks into an educational serious game format:
* **Psychological Horrors (*Silent Hill*, *Blair Witch*):** Replaces basic jump-scares with a dense atmospheric focus and symbolic environments.
* **Cultural Indie Games (*Devotion*, *Mundaun*):** Uses regional mythology, historical records, and geographic isolation to build an authentic setting.
* **Serious Games (*GraphoGame*):** Blends gamification mechanics with learning goals to keep players inherently engaged while absorbing local history.
