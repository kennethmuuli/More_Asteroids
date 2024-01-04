Alvin oli siin!

# Arendus README TODO: Teha korralik README hilisemas järgus.

## Arendusprojekti eesmärgid / koolitöö
- Giti kasutamine:
    - Branching
    - Requestid ja Reviewd
    - Merging
- Mingi tiimitöö tarkvara kasutamine (githubi project)
- Paigaldusjuhis (Git reposse INSTALLATION.MD, lisaks pakitud mängule ka gitist projekti tõmbamine ja Unitysse püsti panek)
- Post Mortem, järel analüüs
- Kasutada tööriistasid: 
    - Unity 2023.2.4f1 
    - VS Code ext. list:
        - C#, C# Dev Kit, IntelliCode for C# Dev Kit
        - Unity (ongi Unity nimega), Unity Tools, Unity Code Snippets

Ideaalis võiks selle itch.io-sse ülesse, ehk lausa mingi raha eest? :O

**Kõige olulisem on justnimelt protsess ise ja koostöö, mitte ilmtingimata projekti valmis saamine. Kasutage erinevaid tööriistasid, kriitilised git, koostöö tarkvara. Arendusejärgselt tuleb kokku panna ka paigaldusjuhis. Analüüsist oluline justnimelt Post Mortem (seda teeb igaüks isiklikult).**

## Mängu üldine sisu
Eesmärk võimalikult kaua ellu jääda üksi, või koos kaaslastega, vältides asteroide. Punkte kogub nii aja (kui kaua oled elus olnud) kui ka asteroidide hävitamise eest.

## MVP
### Game loop:
1) Start
2) Surive/Die (1 hit)
3) Game over

Punktid:
- Aeg, iga 10 sekundit 100 punkti
- Hävitatud asteroidid, 10 punkti

### Tehtavad tööd (üldine):
- Mängija:
    - Liikumine: WASD/Arrow Keys + laeva pööramine (et tulistada õiges suunas)
    - Tulistamine: Space (näiteks), tulistab otse lendava kuuli
        - Kuuli despawn, kui see ekraanilt väjub tuleks see mingi hetk hiljem ka hävitada, et kusagile eetrisse ei jääks hunnikutes füüsika objekte hõljuma
    - Surm/Mäng läbi, kui mängija saab asteroidiga pihta peaks mäng läbi saama (avab mängu menüü)
- Asteroid:
    - Liikumine otsejoones mingis suvalises suunas
        - Suuna määramine, kindlasti peaks see suund olema orienteeritud nähtava ekraani ala suunas, et asteroid kusagil ekraanist väljas ei tekiks ja suvaliselt minema ei lendaks
    - Asteroidi despawn, mängu alast välja lendavad asteroidid tuleks hävitada, et kusagile eetrisse ei jääks hunnikutes füüsika objekte hõljuma
- Skoor:
    - Asteroidi hävitamise sündmus ja punktiskoori loendamine
    - Aja möödudes automaatne punktide juurde lisamine
- UI:
    - Mängu lahti võtmisel avaneb menüü valikutega: Start Game, Quit Game
    - Start Game avab uue mängu.
    - Quit Game paneb kogu mängu kinni.

## Edasiarenduse mõtted
1) Highscore
2) Mida aeg edasi seda raskemaks, ntks: rohkem asteroide ilmub järjest ekraanile
3) Erinevad asteroidid (suurus, punktide arv hävitamise eest, võibolla vajab mitut lasku pihta, et hävitada)
4) Elud, s.t. üks asteroid ei pruugi tähendada automaatselt mäng läbi vaid eemaldam mingi koguse elupunkte
    4.1) Pick-ups: kiirus ajutine, elusid tagasi jne.
5) Multiplayer