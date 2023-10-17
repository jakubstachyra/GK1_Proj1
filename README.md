# GK_Proj1

Edytor wielokątów - podstawowa specyfikacja:

    Możliwość dodawania nowego wielokąta, usuwania oraz edycji \n
    Przy edycji:
        przesuwanie wierzchołka
        usuwanie wierzchołka
        dodawanie wierzchołka w środku wybranej krawędzi
        przesuwanie całej krawędzi
        przesuwanie całego wielokąta
    Dodawanie ograniczeń (relacji) dla wybranej krawędzi:
        możliwe ograniczenia:
        krawędź pozioma, krawędź pionowa
        dwie sąsiednie krawędzie nie mogą być obie pionowe lub obie poziome
        dodawanie wierzchołka na krawędzi lub usuwanie wierzchołka - usuwa ograniczenia "przyległych" krawędzi
        ustawione ograniczenia są widoczne (jako odpowiednie "ikonki") przy środku krawędzi
        powinna istnieć mozliwość usuwania relacji
    Włączanie/wyłączanie wielokąta odsuniętego.
        dla prawidłowego wielokąta (zamknięta łamana bez samoprzecięć) - wielokąt odsunięty nie ma samoprzecięć!
        może istnieć kilka składowych (spójnych) wielokąta odsuniętego
        możliwość płynnej zmiany offsetu (tylko dodatni)
        płynna aktualizacja wielokąta oduniętego podczas modyfikacji wielokąta
    Rysowanie odcinków - algorytm biblioteczny i własna implementacja (alg. Bresenhama) - radiobutton
