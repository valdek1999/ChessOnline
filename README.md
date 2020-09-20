# ChessOnline

Сборник файлов-папок для курсовой по клиент серверному приложению на Unity

ChessWindows - сетевая игра для компьютера на Unity движке.

ChessApi - содержит в себе библиотеку ChessApi.dll, где реализованы шахматные правила

ChessClient - содержит в себе библиотеre ChessClient.dll, где реализована клиентская часть ,которая посылаешь http запросы и принимает ответ на веб-серевере

WebServerApp - содержет веб-сервер приложение ,которые отвечает на http запросы и взаимодействует с бд Chess.

ConsoleChessGame- консольная сетевая игра по шахмам, работает через http://hardfoxy.ddns.net:7777/api/Chess url.

Также можно играть в онлайн шахматы через сайт http://hardfoxy.ddns.net:7777/api/Chess посылая запросы на сервер.

Пример запросов:

http://hardfoxy.ddns.net:7777/api/Chess/{твой id}/ - создать новую игру ,или получить существующую.

http://hardfoxy.ddns.net:7777/api/Chess/{твой id}/{id игры}/{ход} - совершить ход по указанной игре.

Чтобы сделать правильный ход - прочтите FEN- нотацию по ссылке https://en.wikipedia.org/wiki/Fen


