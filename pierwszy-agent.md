# Mój pierwszy agent — karta obserwacji

- Narzędzie i model: Claude Sonnet 4.6
- Moje zadanie (2–3 zdania): Zbuduj minutnik 
- Pierwsza wiadomość (wklejona co do znaku): Zbuduj minutnik w tym projekcie Avalonia. Ma odmierzać 25 minut pracy,
potem 5 minut przerwy, i liczyć skończone cykle. Obie długości mają być do
zmiany w oknie. Jeden przycisk start/pauza, drugi reset. Czas widoczny jako
mm:ss, licznik cykli widoczny obok. Po skończonej pracy przerwa ma ruszyć
sama. Interfejs po polsku, nazwy w kodzie po angielsku, bez dodatkowych
bibliotek. Cała logika wprost w pliku MainWindow.axaml.cs, bez osobnych
klas pośredniczących.
- Co agent zrobił najpierw: Zaczął kompilować i modyfikowac pliki.
- O co pytał — i co zostało zatwierdzone bez czytania: Czy moze uruchomic komende dotnet run
- Pierwszy błąd i co agent z nim zrobił: Pojawil sie blad w plikach avalonia
- Stan po 25 minutach: działa / częściowo / nie działa / nie wiem: Działa
- Skąd wiem, że aplikacja działa (co zostało sprawdzone): Ustawiłam pracę na 1 minutę, wcisnęłam Start. Po minucie licznik
cykli zmienił się na 1 i na liście pojawił się wpis z godziną.
- Rzeczy, które agent zrobił, a których nie rozumiem: Dlaczego ListBox potrzebuje .ToArray() zamiast dostać listę bezpośrednio.
- Jak mi się wydawało, że poszło (jedno zdanie): Mysle ze zrozumialam bardziej jak dziala praca z agentem