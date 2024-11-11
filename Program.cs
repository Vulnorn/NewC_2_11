using System;

namespace NewC_2_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ConsoleMenuCommonAttackPlayer = "1";
            const string ConsoleFireballAttackPlayer = "2";
            const string ConsoleExplosionAttackPlayer = "3";
            const string ConsoleMenuCTreatmentPlayer = "4";
            const int CommonAttackBoss = 1;
            const int PowerfulAttackBoss = 2;
            const int AbilityAttackBoss = 3;
            const int MaxHealthPlayer = 500;
            const int MaxManaPlayer = 100;

            Random random = new Random();

            bool isBattle = true;
            bool isBurn = false;

            int currentHealthPlayer = MaxHealthPlayer;
            int currentManaPlayer = MaxManaPlayer;
            int damageCommonAttackPlayer = 15;
            int damageFireballAttackPlayer = 40;
            int damageManaFireballAttackPlayer = 5;
            int damageExplosionAttackPlayer = 150;
            int damageManaExplosionAttackPlayer = 20;
            int damageTreatmentHealthPlayer = 180;
            int damageTreatmentManaPlayer = 40;
            int numberApplicationsTreatment = 3;
            string inputPlayer;

            int currentHealthBoss = 2000;
            int minRandomAttackBoss = 1;
            int maxRandomAttackBoss = 3;
            int damageCommonAttackBoss = 20;
            int damagePowerfulAttackBoss = 90;
            int damageAbilityAttackBoss = 60;
            int randomAttackBoss;

            Console.WriteLine($"Бой с боссом, у Вас есть умения и атаки для сражения. Бой происходит по ходам, Вы атакуете первым.");

            while (isBattle)
            {
                Console.WriteLine($"___________________________________________________________" +
                    $"\nВаше здоровье - {currentHealthPlayer}; Ваша мана - {currentManaPlayer}." +
                    $"\nЗдорровье босса - {currentHealthBoss}" +
                    $"\n{ConsoleMenuCommonAttackPlayer} Обычная атака наносит {damageCommonAttackPlayer} ед урона." +
                    $"\n{ConsoleFireballAttackPlayer} Огненный шар. Наномит {damageFireballAttackPlayer} ед урона  и тратит {damageManaFireballAttackPlayer} ед маны, накладывает эффект горения на 1 ход." +
                    $"\n{ConsoleExplosionAttackPlayer} Взрыв. Наносит {damageExplosionAttackPlayer} ед урона, тратит {damageManaExplosionAttackPlayer} ед маны, можно использовать если на цели есть эффект горения." +
                    $"\n{ConsoleMenuCTreatmentPlayer} Лечение. Исцеляет {damageTreatmentHealthPlayer} ед хдоровья и восстанавливает {damageTreatmentManaPlayer} ед здоровья игроку. за бой мозно использовать {numberApplicationsTreatment} раза." +
                    $"\nВаш ход, выберите действие.");

                inputPlayer = Console.ReadLine();

                switch (inputPlayer)
                {
                    case ConsoleMenuCommonAttackPlayer:
                        currentHealthBoss -= damageCommonAttackPlayer;
                        isBurn = false;
                        Console.WriteLine($"Вы наносите боссу {damageCommonAttackPlayer} урона.");
                        break;

                    case ConsoleFireballAttackPlayer:
                        if (currentManaPlayer >= damageManaFireballAttackPlayer)
                        {
                            currentHealthBoss -= damageFireballAttackPlayer;
                            currentManaPlayer -= damageManaFireballAttackPlayer;
                            isBurn = true;
                            Console.WriteLine($"Вы наносите боссу {damageFireballAttackPlayer} урона.");
                        }
                        else
                        {
                            Console.WriteLine($"Не достаточно маны, атака не доступна.");
                            isBurn = false;
                        }

                        break;

                    case ConsoleExplosionAttackPlayer:
                        if (isBurn == true)
                        {
                            if (currentManaPlayer >= damageManaExplosionAttackPlayer)
                            {
                                currentHealthBoss -= damageExplosionAttackPlayer;
                                currentManaPlayer -= damageManaExplosionAttackPlayer;
                                Console.WriteLine($"Вы наносите боссу {damageExplosionAttackPlayer} урона.");
                            }
                            else Console.WriteLine($"Не достаточно маны, атака не доступна.");
                        }
                        else Console.WriteLine($"Цель не горит, атака не возможна.");

                        isBurn = false;
                        break;

                    case ConsoleMenuCTreatmentPlayer:
                        if (numberApplicationsTreatment > 0)
                        {
                            currentHealthPlayer += damageTreatmentHealthPlayer;
                            currentManaPlayer += damageTreatmentManaPlayer;
                            numberApplicationsTreatment--;

                            if (currentHealthPlayer > MaxHealthPlayer) { currentHealthPlayer = MaxHealthPlayer; }

                            if (currentManaPlayer > MaxManaPlayer) { currentManaPlayer = MaxManaPlayer; }

                            Console.WriteLine($"Вы восстановили себе здоровье и ману.");
                        }
                        else Console.WriteLine($"Не достаточно зарядов, исцеление не доступно.");

                        isBurn = false;
                        break;

                    default:
                        Console.WriteLine($"Такой команды нет, вы пропускаете ход.");
                        isBurn = false;
                        break;

                }

                randomAttackBoss = random.Next(minRandomAttackBoss, maxRandomAttackBoss + 1);

                switch (randomAttackBoss)
                {
                    case CommonAttackBoss:
                        currentHealthPlayer -= damageCommonAttackBoss;
                        Console.WriteLine($"Босс наносит Вам {damageCommonAttackBoss} урона.");
                        break;

                    case PowerfulAttackBoss:
                        currentHealthPlayer -= damagePowerfulAttackBoss;
                        Console.WriteLine($"Босс наносит Вам {damagePowerfulAttackBoss} урона.");
                        break;

                    case AbilityAttackBoss:
                        currentHealthPlayer -= damageAbilityAttackBoss;

                        if (isBurn == true)
                        {
                            isBurn = false;
                            Console.WriteLine($"Восс снимает с себя дебаф горения");
                        }

                        Console.WriteLine($"Босс наносит Вам {damageAbilityAttackBoss} урона.");
                        break;
                }

                if (currentHealthPlayer<= 0 || currentHealthBoss <= 0) { isBattle = false; }
            }

            Console.WriteLine($"Бой закончился.");

            if (currentHealthBoss > 0 && currentHealthPlayer <= 0) { Console.WriteLine($"Вы проиграли. Вы погибли"); }
            else if (currentHealthPlayer > 0 && currentHealthBoss <= 0) { Console.WriteLine($"Вы выиграли. Босс повержен"); }
            else if (currentHealthPlayer <= 0 && currentHealthBoss <= 0) { Console.WriteLine($"Ничья, оба противника погибли."); }

            Console.ReadKey();
        }
    }
}