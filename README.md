# **ZombieOutbreak**

## Windows Forms Project by: Tamara Stojanoska (231030), Davor Lazarov (233109) and Ognen Mladenovski (233108)

### Tutorial: https://www.youtube.com/watch?v=z2h_4_f6fD8 <br>

# **Македонски**

## 1. Опис на апликацијата
Проектната задача претставува имплементација на игра со зомби, изработена со помош на **C#** и **Windows Forms**. Главната цел на играчот е да преживее колку што е можно подолго, избегнувајќи зомби суштества кои се појавуваат на екранот и се движат кон него. 

**Начин на играње**:
#### 1. Играта започнува со fullscreen режим.

#### 2. Првиот екран содржи копчиња: играј, излез и начин на играње. Има можност за избор на јазик (Македонски и Англиски) и музика додека не започне играта (песна: The Walking Dead Season 3 Comic Con Trailer Song Black - Kari Kimmel).
<img width="1430" height="894" alt="pic1" src="https://github.com/user-attachments/assets/dffe95f7-5692-450d-9756-747a74a9d36e" /><br>
<img width="1430" height="894" alt="pic2" src="https://github.com/user-attachments/assets/aa2ba6cb-58c3-4ab8-bd4d-2cf9b127182c" />

#### 3.	Се стартува играта со клик на копчето “Играј”.

#### 4.	Се појавува нов екран на кој има можност за избор Од три вида на оружје, секој со свои карактеристики:
-	**Снајпер**: кој испукува 1 куршум и има долг опсег. 
-	**Сачмарка**: која испукува 5 куршуми под различен агол и во различна насока со пократок опсег. 
-	**Автомат**: кој испукува повеќе куршуми во иста насока, под ист агол и со краток опсег.
<img width="1430" height="894" alt="pic3" src="https://github.com/user-attachments/assets/336270a6-0190-424a-9285-da86ee3fbd9a" />

#### 5.	По одлуката за оружје, со клик на копчето Избери започнува играта со соодветно избраното оружје.
<img width="1430" height="894" alt="pic4" src="https://github.com/user-attachments/assets/da38276f-216d-48f7-8711-8de453a16f74" /><br>
<img width="1430" height="894" alt="pic5" src="https://github.com/user-attachments/assets/bce02419-2a3a-4c2c-8338-3b20da11c659" /><br>
<img width="1430" height="894" alt="pic6" src="https://github.com/user-attachments/assets/19917f3a-ba42-458c-9c1b-5fa6413d69c2" /><br>

#### 6.	Се користат тастерите ← ↑ ↓ → за движење.

#### 7.	Со клик на копчето SPACE се пука.

#### 8.	При убиени 10 зомбиња пoминувате кон повисоко ниво и со тоа играта се отежнува (доаѓаат поголем број зомбиња и истите се движат побрзо).

#### 9.	На секои 20тина секунди се појавуваат предмети за засилување/бонуси:
-	**Златна**, **сребрена** и **бронзена кутија** - Бонус за **10**, **5** и **3** куршуми.
-	**Зелена кутија** - Бонус со кој се **продолжува животот**.
-	**Плава кутија** - Бонус со кој **зомбињата прекинуваат со движење на одредено време** притоа играчот за тоа време може да ги пука.
Доколку не се соберат за одредено време бонусите исчезнуваат од екранот.

#### 10.	Има 6 различни средини кои се генерираат на случаен начин при секој нов почеток на играта.

#### 11.	Играта завршува кога животот на играчот ќе истече.

#### 12.	Се појавува екран Играта е завршена со можност за рестарт, избор на ново оружје и излез од играта.

<img width="1430" height="894" alt="pic7" src="https://github.com/user-attachments/assets/26044f8c-aaa3-488d-8bcc-3bd2daeca9b9" /><br>

**Идејна инспирација**: Играта е инспирирана од класични зомби игри како “World War Z” или “Left 4 Dead”, но со poедноставен 2D интерфејс.

## 2. Опис на решението
За реализација на играта се искористени **објектно-ориентирани принципи**. Целта е да се овозможи динамично прикажување на различни сцени и функционалности преку **шест различни форми**, при што секоја форма претставува одредена состојба во играта.

<p align="center">
  <img width="400" height="360" alt="pic8" src="https://github.com/user-attachments/assets/be73b132-2a08-4814-ab44-212642cae3f0" />
</p>

## Дополнителни класи во играта:
### 1. Bullet – Куршум за снајпер и автомат:
Се користи за моделирање на обични куршуми што се испукуваат од **снајпер** или **автомат**. Куршумите имаат своја позиција, брзина и насока на движење.

### 2. ShotgunPellet – Мали куршуми за сачмарка:
**Сачмарката** пука не еден, туку повеќе пелети (мали куршуми) со различен агол и насока. Оваа класа моделира еден таков пелет.

## Чување на ресурси:
- **Слики**: се чуваат во посебни фолдери и се вчитуваат динамички (пр. Resources/zombies/, Resources/backgrounds/).
- **Звуци**: .wav фајлови за музика.
- **Јазици**: текстови и инструкции организирани преку услови според Language.

## 3. Опис на функција/класа 
**Метод за создавање зомби – MakeZombies()**, тој е одговорен за создавање на поединечни зомби-елементи во форма на PictureBox контроли кои се поставуваат на случајни локации на формата, без да се преклопуваат со играчот или друго зомби. Овие зомби се визуелно прикажани преку слики и се додаваат во листата zombiesList, како и на самата форма.
```C#
private void MakeZombies()
{
    string[] types = { "z", "m", "b", "ar" };
    string type = types[randNum.Next(types.Length)];
    int frame = randNum.Next(4);
    Image zombieImage = zombieImages[type][frame];

    int attempts = 0;
    const int maxAttempts = 100;

    while (attempts < maxAttempts)
    {
        attempts++;

        int x = randNum.Next(0, this.ClientSize.Width - 170);
        int y = randNum.Next(50, this.ClientSize.Height - 170);

        Rectangle newZombieRect = new Rectangle(new Point(x, y), new Size(170, 170));

        bool intersects = false;

        if (newZombieRect.IntersectsWith(player.Bounds))
        {
            intersects = true;
        }

        if (!intersects)
        {
            foreach (PictureBox existingZombie in zombiesList)
            {
                if (newZombieRect.IntersectsWith(new Rectangle(existingZombie.Location, existingZombie.Size)))
                {
                    intersects = true;
                    break;
                }
            }
        }

        if (!intersects)
        {
            PictureBox zombie = new PictureBox
            {
                Tag = $"zombie:{type}",
                Image = zombieImage,
                Left = x,
                Top = y,
                Size = new Size(170, 170),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Parent = this
            };

            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
            zombie.BringToFront();
            player.BringToFront();
            return;
        }
    }
}

```

### 1.	Избор на тип на зомби:
- Случаен избор од низа типови на зомби (на пример: "z", "m", "b" и "ar").
- Се избира и случаен фрејм за соодветната анимација.
  
### 2.	Максимален број обиди:
- Ако не се најде валидна позиција по 100 обиди, зомбито не се создава (за да се избегне бесконечна итерација).

## 4. Употреба на Генеративна Вештачка Интелигенција
За време на развојот, користена е **генеративна вештачка интелигенција – ChatGPT од OpenAI** за помош со следниве аспекти:
Употребени **Prompt-ови**:
#### 1.	„Како да направам collision detection помеѓу PictureBox објекти?“
#### 2.	„Како да генерирам агол на движење за сачмарката за да симулирам распрснување?“
#### 3.	„Како да овозможам double buffering во Windows Forms за да спречам трепкање при анимација?“
#### 4.	„Како да вчитам слика од Resources во WinForms ако е зачувана како бинарен ресурс (byte[])?“
#### 5.	„Како да ротираш Image објект во C# WinForms за одреден агол (на пр. 90 степени)? Сакам да ја прикажам ротираната слика во PictureBox.“

**Напомена**:
Сите визуелни компоненти, вклучително и позадините, играчот и зомбињата, се креирани користејќи посебен софтвер за уредување слики.


<br>
<br>
<br>


# **English**

## 1.Problem Description (Application/Game)
The project task is the implementation of a zombie-themed game, developed using **C#** and **Windows Forms**. The main goal of the player is to survive as long as possible, avoiding zombie creatures that appear on the screen and move toward them.

**Gameplay**:
#### 1. The game starts in fullscreen mode.

#### 2. The first screen contains buttons: Play, Exit, and How to Play. There is a language option (Macedonian ор English) and background music before the game begins (Song: The Walking Dead Season 3 Comic Con Trailer Song - Black by Kari Kimmel).
<img width="1430" height="894" alt="pic1" src="https://github.com/user-attachments/assets/dffe95f7-5692-450d-9756-747a74a9d36e" /><br>
<img width="1430" height="894" alt="pic2" src="https://github.com/user-attachments/assets/aa2ba6cb-58c3-4ab8-bd4d-2cf9b127182c" />

#### 3.	The game starts by clicking the “Play” button.

#### 4.	A new screen appears where the player can choose between three types of weapons (Single Action Rifle, Shotgun, and UZI SMG), each with unique characteristics:
-	**Single Action Rifle**: which fires 1 bullet with a long range.
-	**Shotgun:**: which fires 5 pellets at different angles and directions, with shorter range.
-	**UZI SMG**: which fires multiple bullets in the same direction, with the same angle and short range.
<img width="1430" height="894" alt="pic3" src="https://github.com/user-attachments/assets/336270a6-0190-424a-9285-da86ee3fbd9a" />

#### 5.	After selecting a weapon and clicking the Select button, the game begins (a new screen appears).
<img width="1430" height="894" alt="pic4" src="https://github.com/user-attachments/assets/da38276f-216d-48f7-8711-8de453a16f74" /><br>
<img width="1430" height="894" alt="pic5" src="https://github.com/user-attachments/assets/bce02419-2a3a-4c2c-8338-3b20da11c659" /><br>
<img width="1430" height="894" alt="pic6" src="https://github.com/user-attachments/assets/23bd1098-b6eb-4344-93f3-49b33722f33c" /><br>

#### 6.	Movement is controlled using the ← ↑ ↓ → arrow keys.

#### 7.	Pressing the SPACE key fires the weapon.

#### 8.	After killing 10 zombies, the player progresses to the next level, and the game becomes harder (more zombies appear, and they move faster).

#### 9.	Every ~20 seconds, power-ups spawn on the screen:
-	**Gold**, **silver** и **bronze box** - gives the player a bonus of **10**, **5** or **3** bullets.
-	**Green box** - health power-up that **restores life**.
-	**Blue box** - **freeze power-up that stops zombies temporarily**, allowing the player to shoot freely.
If not collected in time, the power-ups disappear from the screen.

#### 10.	There are 6 different environments that are randomly selected at the start of game.

#### 11.	The game ends when the player's health reaches zero.

#### 12.	A Game Over screen appears with options: restart, choose a new weapon, or quit.

<img width="1430" height="894" alt="pic7" src="https://github.com/user-attachments/assets/26044f8c-aaa3-488d-8bcc-3bd2daeca9b9" /><br>

**Inspirational Note**: The game is inspired by classic zombie games such as "World War Z" and "Left 4 Dead", but implemented with a simple 2D interface.

## 2. Solution Description
The implementation uses **object-oriented programming principles**. The goal is to dynamically display different scenes and features across **six separate forms**, where each form represents a specific game state.

<p align="center">
  <img width="400" height="360" alt="pic8" src="https://github.com/user-attachments/assets/be73b132-2a08-4814-ab44-212642cae3f0" />
</p>

## Additional Classes in the Game:
### 1. Bullet – Bullet for Single Action Rifle or UZI SMG:
Used to model regular bullets fired from the **Single Action Rifle** or **UZI SMG**. Bullets have position, speed, and direction.

### 2. ShotgunPellet – Small pellets fired from the shotgun:
The **Shotgun** fires multiple pellets with varying angles and directions. This class models a single pellet.

## Resource Handling:
- **Images**: stored in separate folders and loaded dynamically (e.g. Resources/zombies/, Resources/backgrounds/)
- **Audio**: .wav files for music and sound effects.
- **Languages**: Menu texts and instructions are handled through conditions based on selected language.

## 3. Function/Class Description
**Zombie Creation Method – MakeZombies()**, responsible for generating zombie elements as PictureBox controls at random positions on the form, without overlapping with the player or existing zombies. Each zombie is displayed with a corresponding image and added to both the zombiesList and the form.
```C#
private void MakeZombies()
{
    string[] types = { "z", "m", "b", "ar" };
    string type = types[randNum.Next(types.Length)];
    int frame = randNum.Next(4);
    Image zombieImage = zombieImages[type][frame];

    int attempts = 0;
    const int maxAttempts = 100;

    while (attempts < maxAttempts)
    {
        attempts++;

        int x = randNum.Next(0, this.ClientSize.Width - 170);
        int y = randNum.Next(50, this.ClientSize.Height - 170);

        Rectangle newZombieRect = new Rectangle(new Point(x, y), new Size(170, 170));

        bool intersects = false;

        if (newZombieRect.IntersectsWith(player.Bounds))
        {
            intersects = true;
        }

        if (!intersects)
        {
            foreach (PictureBox existingZombie in zombiesList)
            {
                if (newZombieRect.IntersectsWith(new Rectangle(existingZombie.Location, existingZombie.Size)))
                {
                    intersects = true;
                    break;
                }
            }
        }

        if (!intersects)
        {
            PictureBox zombie = new PictureBox
            {
                Tag = $"zombie:{type}",
                Image = zombieImage,
                Left = x,
                Top = y,
                Size = new Size(170, 170),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Parent = this
            };

            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
            zombie.BringToFront();
            player.BringToFront();
            return;
        }
    }
}

```

### 1.	Zombie Type Selection:
- Randomly selects a zombie type (e.g. "z", "m", "b" and "ar").
- Randomly selects an animation frame.
  
### 2.	Max Attempts:
- If a valid position is not found after 100 tries, the zombie is not created (to avoid infinite loop).
  
## 4. Use of Generative Artificial Intelligence
During development, **generative AI – ChatGPT by OpenAI** – was used for support with the following aspects:
**Prompts Used**:
#### 1. „How to do collision detection between PictureBox objects?“
#### 2.	„How to generate random angle for shotgun spread simulation?“
#### 3.	„How to enable double buffering in Windows Forms to prevent flickering during animation?“
#### 4.	„How to load an image from Resources in WinForms if it’s stored as a binary resource (byte[])?“
#### 5.	„How to rotate an Image object in C# WinForms by a certain angle (e.g. 90 degrees)? I want to show the rotated image in a PictureBox.“

**Note**:
All visual components, including backgrounds, the player, and zombies, were created using external image editing software.

