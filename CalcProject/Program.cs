using CalcProject.App;

// Створення об'єктів для ін'єкції
Resources Resources = new();
// Передача ресурсів у роботу програми
new CalcProject.App.Calc(Resources).Run();


// Comment from github
// Comment from VS

/*
 * Ідея - калькулятор "незвичайних" чисел
 * 1. Римські числа
 * 2? (буде/не буде)
 * 
 * ХР - не робити "запас", реалізовувати буквально те, що
 * вимагається
 */
