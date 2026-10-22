# Minutnik Pomodoro — 3a

Aplikacja desktopowa Avalonia odmierzająca cykle pracy i przerwy (Pomodoro).

## Jak uruchomić

```
dotnet run --project 3a.csproj
```

## Siedem zasad

1. Praca 25 min, przerwa 5 min — obie wartości do zmiany w oknie
2. Jeden przycisk Start / Pauza, drugi Reset
3. Widoczny licznik skończonych cykli
4. Po skończonej pracy przerwa startuje sama
5. Czas pokazywany jako `mm:ss`
6. Interfejs po polsku, nazwy w kodzie po angielsku
7. Żadnych dodatkowych bibliotek

## Liczby z lekcji (stan na 2026-10-22, narzędzie: Claude Sonnet 4.6)

### Tokeny — pierwsza wiadomość do agenta

Zmierzone na [Tokenizer Playground](https://huggingface.co/spaces/Xenova/the-tokenizer-playground),
model: `cl100k_base` (odpowiednik Claude/GPT-4).

| Wersja wiadomości | Liczba tokenów |
|---|---|
| Po polsku | 201|
| Po angielsku | 140 |

> Polska wiadomość zaczyna się od: „Zbuduj minutnik w tym projekcie Avalonia…"

### Okno kontekstu i limit odpowiedzi — Claude Sonnet 4.6

| Parametr | Wartość |
|---|---|
| Okno kontekstu (input) | 1 000 000 tokenów |
| Maksymalna odpowiedź (output) | 128 000 tokenów |

Źródło: [Models overview — Anthropic](https://platform.claude.com/docs/en/about-claude/models/overview)
(sprawdzone 2026-10-22)

## Test pola liczbowego: przecinek i kropka

| Wpisana wartość | Co się stało |
|---|---|
| `25,5` | Nic sie nie stalo, nie zaokraglilo |
| `25.5` | Nic sie nie stalo |

> Wskazówka: pole przyjmuje tylko całe liczby (kod rzutuje wartość na `int`).
> Zachowanie separatora dziesiętnego zależy od ustawień regionalnych systemu.
