using S28Maker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace S28Maker.Tests
{
    public class S28DocumentParserTests
    {
        private const string S28Template2019 = "ЕЖЕМЕСЯЧНЫЙ УЧЕТ ЛИТЕРАТУРЫ\nИНСТРУКЦИИ: ЯЗЫК: _______________________\n1. Собранию, координирующему языковую литературную группу необходимо каждый месяц подсчитывать количество имеющийся\nлитературы и вписывать это количество в колонку «В наличии».\n2. В колонке «Получено» следует указывать количество экземпляров каждого полученного или возвращенного предмета.\n3. В колонке «Выдано» требуется обозначить количество каждого выданного предмета. Это количество можно вычислить, сложив то, что\nбыло получено в течение месяца, с тем, что было в наличии в предыдущем месяце, и затем, вычтя из полученной суммы количество\nоставшихся в наличии предметов (колонка «В наличии»).\n4. Заказывать предметы нужно так, чтобы они всегда были в запасе, но не более чем на три месяца. Предметы по индивидуальному\nзапросу обозначены звездочкой (*). Их не следует заказывать про запас.\nВАЖНО: Данные об инвентаризации следует отправлять дважды в год через сайт jw.org. Отправляйте, пожалуйста,\nв филиал отчет об инвентаризации за февраль/август не позднее 31 марта/30 сентября.\nсент/март окт/апр нояб/май дек/июнь янв/июль февр/авг\nМЕСЯЦ И ГОД\nПредмет\nВ В В В В В В\nЕжегодные предметы\n№\nналичии Получ. наличии Выдано Получ. наличии Выдано Получ. наличии Выдано Получ. наличии Выдано Получ. наличии Выдано Получ. наличии Выдано\nПример 212 +0 -181 =31 0 140 41 0 32 108 100 111 21 0 78 33 100 70 108\n5000 Электронная Библиотека (все годы)*\n5001 Исследуем Писания (текущий год)*\n5005 Ежегодники (все годы)*\n5006 Переплетенные тома (текущий год)*\n5007 Переплетенные тома (все другие годы)*\n5008 Индекс (за несколько лет)*\n5009 Индекс (за один год)*\nБиблии\n5109 ПНМ (большой формат)* bi8\nbi12\n5116 ПНМ в мягкой обложке\nDLbi8\n5118 ПНМ  (роскошное издание, серый, большой формат) *\nDLbi12\n5117 ПНМ (роскошное издание, серый)\n5112 ПНМ в твердой обложке Hbi12\n5100 Другие Библии\nКниги\n5414 Школа служения be\n5334 Чему учит Библия bh\n5340 Чему нас учит Библия bhs\n5416 Основательно свидетельствуем bt\n5231 «Следуй за мной» cf\n5331 Приближайся cl\n5228 Творец ct\ndp\n5328 Пророчество Даниила\nfy\n5227 Семейное счастье\ngm\n5225 Слово Бога\n5327 Самый великий человек gt\nia\n5419 Подражайте\n5329 Пророчество Исаии (том I) ip-1\n5330 Пророчество Исаии (том II) ip-2\n5230 День Єгови jd\n5232 Иеремия jr\njy\n5425 Иисус — путь\n5422 Царство Бога правит! kr\nlfb\n5427 Учимся на примерах из Библии\nlr\n5415 Великий Учитель\nlv\n5335 Божья любовь\n5343 Оставайтесь в любви Бога lvs\n5407 Библейские рассказы my\n5332 Организованы* od\n5410 Апогей Откровения re\n5435 Чистое поклонение rr\n5323 Рассуждение rs\nSce\n5324 Сотворение (маленькая)\nsh\n5326 Человечество в поисках Бога\nsi\n5403 «Все Писание»\n5341 Радостно пойте sjj\nsjjls\n5441 Радостно пойте (большой формат)\n5442 Радостно пойте (тексты песен) sjjyls\n5322 Библейские рассказы (маленькая) Smy\n5325 Ответы на твои вопросы yp\n5339 Ответы на твои вопросы. Том 1 yp1\nyp2\n5336 Ответы на твои вопросы. Том 2\n5200 Другие книги\nКниги в крупном шрифте\n6200 Все книги в крупном шрифте*\nБрошюры\n6641 Книга для всех ba\n6652 Главная тема Библии bm\n6623 Правительство bp\ndg\n6632 Заботится ли Бог\ned\n6638 Образование\n6659 Добрая весть fg\nS-28-U Uk   2/19\nсент/март окт/апр нояб/май дек/июнь янв/июль февр/авг\nМЕСЯЦ И ГОД\nПредмет\nВ В В В В В В\nБрошюры (продолжение)\n№\nналичии Получ. наличии Выдано Получ. наличии Выдано Получ. наличии Выдано Получ. наличии Выдано Получ. наличии Выдано Получ. наличии Выдано\n6645 Друг Бога gf\n6650 «Добрая земля»* gl\nhb\n6628 Кровь\nhf\n6665 Счастливая семья\nie\n6642 Что происходит при смерти?\n6660 Воля Иеговы jl\n6547 Первое знакомство со Словом Бога igw\n6548 Карты и схемы sgd\n6651 Бодрствуйте! kp\nla\n6647 Удовлетворение в жизни\nlc\n6654 Была ли жизнь создана?\nld\n6658 Слушайся Бога\n6620 Радуйся жизни на земле вечно! le\n6655 У истоков жизни lf\n6657 Слушайся и живи ll\nLmn\n6625 «Смотри!»\nmb\n6663 Библия для самых маленьких\nna\n6622 Божье имя\n6674 Благая весть для людей из всех народов np\n6636 Смысл жизни pr\n6671 Вернись к Иегове rj\n6656 Настоящая вера rk\nsp\n6630 Духи мертвых\nth\n6667 Развивай навыки чтения и способность учить\nti\n6627 Троица\n6637 Когда умер близкий тебе человек we\n6634 Мир без войны wi\n6664 Обучайте своих детей yc\nypq\n6684 10 вопросов\n6600 Другие брошюры\nБуклеты (2,5 см = 300 штук)\n7305 Приглашение на встречи собрания inv\n7074 Узнать истину kt\nT-13\n7113 Доверять Библии\nT-14\n7114 Свидетели Иеговы\nT-15\n7115 Жизнь в мирном новом мире\n7116 Надежда для умерших T-16\n7117 Мирный новый мир. Наступит ли он? T-17\n7118 Во что верят Свидетели Иеговы? T-18\n7119 Выживет ли этот мир? T-19\n7120 Утешение страдающим депрессией T-20\n7121 Радуйся семейной жизни T-21\nT-22\n7122 Кто правит миром?\nT-23\n7123 Кто такой Иегова?\nT-24\n7124 Кто такой Иисус Христос?\n7125 Бессмертный дух T-25\nT-27\n7127 Страданий не будет\n7130 Что для вас Библия? T-30\n7131 Каким вы видите будущее? T-31\n7132 На чем строится семейное счастье? T-32\n7133 В чьих руках этот мир? T-33\n7134 Придет ли конец страданиям? T-34\n7135 Будут ли умершие жить снова? T-35\nT-36\n7136 Что такое Царство Бога?\nГде найти ответы на самые важные T-37\n7137\nвопросы?\nT-71\n7171 Находится ли все в руках судьбы?\n7173 Кто такие Свидетели Иеговы? T-73\n7174 Огненный ад T-74\n7100 Другие буклеты\nDVD\n9200 Все DVD (кроме Сторожевой башни)*\nDVD (Жестовый язык)\n9305 Чему нас учит Библия? (на DVD) dvbhs\n9280 Добрая весть от Бога (на DVD) dvfg\n9270 Слушайся Бога и живи вечно (на DVD) dvll\n1540 Благая весть в изложении Матфея (на DVD) dvMt\n9295 Вопросы, на которые отвечает Библия (на DVD) dvqua7\nБланки (2,5 см = 300 штук)\nS-24\n8724 Квитанция\n8704 Отчет о проповедническом служении S-4\nПЕРИОДИЧЕСКИЕ ИЗДАНИЯ             Отчетный месяц: янв/февр март/апр май/июнь июль/авг сент/окт нояб/дек\nСторожевая башня Пробудитесь! Сторожевая башня Пробудитесь! Сторожевая башня Пробудитесь!\nВпишите общее количество каждого полученного выпуска журнала.\nвыпуск № 1 выпуск № 1 выпуск № 2 выпуск № 2 выпуск № 3 выпуск № 3\nВпишите оставшееся количество каждого выпуска, в конце указанных\nмесяцев. Получ. Остаток Выдано Получ. Остаток Выдано Получ. Остаток Выдано Получ. Остаток Выдано Получ. Остаток Выдано Получ. Остаток Выдано\n8000 2018 Журналы для распространения (печатные)\n9000 2019 Журналы для распространения (печатные)\nS-28-U Uk   2/19";
        [Fact]
        public void TestParsePublicationNames()
        {
            var publicationNames = S28PdfTextExtractor.ExtractNames(S28Template2019);

            Assert.Equal(125, publicationNames.Count);
        }

        [Fact]
        public void TestParsePublicationNames_CheckFirstOnFirstPage()
        {
            var publicationNames = S28PdfTextExtractor.ExtractNames(S28Template2019);

            Assert.Equal("Электронная Библиотека (все годы)*", publicationNames[0].Name);
        }

        [Fact]
        public void TestParsePublicationNames_CheckBibleOnFirstPage()
        {
            var publicationNames = S28PdfTextExtractor.ExtractNames(S28Template2019);

            Assert.Equal("ПНМ (большой формат)* bi8", publicationNames[7].Name);
        }

        [Fact]
        public void TestParsePublicationNames_CheckBooksOnFirstPage()
        {
            var publicationNames = S28PdfTextExtractor.ExtractNames(S28Template2019);

            Assert.Equal("Школа служения be", publicationNames[13].Name);
        }

        [Fact]
        public void TestParsePublicationNames_CheckIgnoredBook()
        {
            var testTemplateWithIgnored = "5008 Индекс (за несколько лет)*\n5200 Другие книги\n";
            var publicationNames = S28PdfTextExtractor.ExtractNames(testTemplateWithIgnored);

            Assert.Single(publicationNames);
            Assert.Contains(publicationNames, pub => pub.Name == "Индекс (за несколько лет)*");
            Assert.DoesNotContain(publicationNames, pub => pub.Name == "Другие книги");
        }

        [Fact]
        public void TestParsePublicationNames_CheckLargeBooksOnFirstPage()
        {
            var publicationNames = S28PdfTextExtractor.ExtractNames(S28Template2019);

            Assert.Equal("Все книги в крупном шрифте*", publicationNames[50].Name);
        }

        [Fact]
        public void TestParsePublicationNames_CheckFirstOnSecondPage()
        {
            var publicationNames = S28PdfTextExtractor.ExtractNames(S28Template2019);

            Assert.Equal("Друг Бога gf", publicationNames[57].Name);
        }

        [Fact]
        public void TestParsePublicationNames_CheckBookletOnSecondPage()
        {
            var publicationNames = S28PdfTextExtractor.ExtractNames(S28Template2019);

            Assert.Equal("Приглашение на встречи собрания inv", publicationNames[87].Name);
        }

        [Fact]
        public void TestParsePublicationNames_CheckDvdOnSecondPage()
        {
            var publicationNames = S28PdfTextExtractor.ExtractNames(S28Template2019);

            Assert.Equal("Все DVD (кроме Сторожевой башни)*", publicationNames[115].Name);
        }
        [Fact]
        public void TestParsePublicationNames_CheckDvdSignLanguageOnSecondPage()
        {
            var publicationNames = S28PdfTextExtractor.ExtractNames(S28Template2019);

            Assert.Equal("Чему нас учит Библия? (на DVD) dvbhs", publicationNames[116].Name);
        }
        [Fact]
        public void TestParsePublicationNames_CheckReceiptLanguageOnSecondPage()
        {
            var publicationNames = S28PdfTextExtractor.ExtractNames(S28Template2019);

            Assert.Equal("Квитанция", publicationNames[121].Name);
        }

        [Fact]
        public void TestParsePublicationNames_CheckLastOnFirstPage()
        {
            var publicationNames = S28PdfTextExtractor.ExtractNames(S28Template2019);

            Assert.Equal("Добрая весть fg", publicationNames[56].Name);
        }
    }
}
