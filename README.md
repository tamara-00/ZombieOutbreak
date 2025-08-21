# **ZombieOutbreak**

## Windows Forms Project by: Tamara Stojanoska (231030), Davor Lazarov (233109) and Ognen Mladenovski (233108)

### Tutorial: https://www.youtube.com/watch?v=z2h_4_f6fD8

**Македонски**

## 1. Опис на апликацијата
Проектната задача претставува имплементација на игра со зомби, изработена со помош на **C#** и **Windows Forms**. Главната цел на играчот е да преживее колку што е можно подолго, избегнувајќи зомби суштества кои се појавуваат на екранот и се движат кон него. 

**Начин на играње**:
#### 1. Играта започнува со fullscreen режим.

#### 2. Првиот екран содржи копчиња: **играј**, **излез** и **начин на играње**. Има можност за избор на **јазик** (Македонски и Англиски) и музика додека не започне играта (песна: The Walking Dead Season 3 Comic Con Trailer Song Black - Kari Kimmel).
<img width="1430" height="894" alt="pic1" src="https://github.com/user-attachments/assets/dffe95f7-5692-450d-9756-747a74a9d36e" /><br>
<img width="1430" height="894" alt="pic2" src="https://github.com/user-attachments/assets/aa2ba6cb-58c3-4ab8-bd4d-2cf9b127182c" />

#### 3.	Се стартува играта со клик на копчето **“Играј”**.

#### 4.	Се појавува нов екран на кој има можност за избор Од три вида на оружје, секој со свои карактеристики:
-	**Снајпер**: кој испукува 1 куршум и има долг опсег. 
-	**Сачмарка**: која испукува 5 куршуми под различен агол и во различна насока со пократок опсег. 
-	**Автомат**: кој испукува повеќе куршуми во иста насока, под ист агол и со краток опсег.
<img width="1430" height="894" alt="pic3" src="https://github.com/user-attachments/assets/336270a6-0190-424a-9285-da86ee3fbd9a" />

#### 5.	По одлуката за оружје, со клик на копчето Избери започнува играта со соодветно избраното оружје.
<img width="1430" height="894" alt="pic4" src="https://github.com/user-attachments/assets/da38276f-216d-48f7-8711-8de453a16f74" /><br>
<img width="1430" height="894" alt="pic5" src="https://github.com/user-attachments/assets/bce02419-2a3a-4c2c-8338-3b20da11c659" /><br>
<img width="1430" height="894" alt="pic6" src="https://github.com/user-attachments/assets/3b575301-ce02-4d76-bdbc-adc306da544d" /><br>

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
За реализација на играта се искористени објектно-ориентирани принципи. Целта е да се овозможи динамично прикажување на различни сцени и функционалности преку шест различни форми, при што секоја форма претставува одредена состојба во играта.

<p align="center">
  <img width="400" height="360" alt="pic8" src="https://github.com/user-attachments/assets/be73b132-2a08-4814-ab44-212642cae3f0" />
</p>

## Дополнителни класи во играта
### 1. Bullet – Куршум за снајпер и автомат
Се користи за моделирање на обични куршуми што се испукуваат од снајпер или автомат. Куршумите имаат своја позиција, брзина и насока на движење.

### 2. ShotgunPellet – Мали куршуми за сачмарка
Сачмарката пука не еден, туку повеќе пелети (мали куршуми) со различен агол и насока. Оваа класа моделира еден таков пелет.

## Чување на ресурси
- **Слики**: се чуваат во посебни фолдери и се вчитуваат динамички (на пр. Resources/zombies/, Resources/backgrounds/).
- **Звуци**: .wav фајлови за музика.
- **Јазици**: текстови и инструкции организирани преку услови според Language.

## Опис на функција/класа 
**Метод за создавање зомби – MakeZombies()**, одговорен за создавање на поединечни зомби-елементи во форма на PictureBox контроли кои се поставуваат на случајни локации на формата, без да се преклопуваат со играчот или други зомби. Овие зомби се визуелно прикажани преку слики и се додаваат во листата zombiesList, како и на самата форма.
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
