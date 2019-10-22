# PolygonEditor
PolygonEditor to projekt stworzony w ramach pierwszego laboratorium z przedmiotu Grafika Komputerowa 1. Jest to edytor do tworzenia wielokątów, ich modyfikacji z opcją wiązania relacji pomiędzy krawędziami.

## Funkcjonalność
Edytor zapewnia poniższe funkcjonalności:
* Przy edycji  
    * Przesuwanie wierzchołka
    * Usuwanie wierzchołka
    * Dodawanie wierzchołka w środku wybranej krawędzi
    * Przesuwanie całej krawędzi
    * Przesuwanie całego wielokąta
* Dodawanie ograniczeń (relacji) dla wybranej pary krawędzi:
    * Równa długość krawędzi
    * Krawędzie prostopadłe
    
## Użycie
Zmiana funkcjonalności odbywa się poprzez wybór odpowiedniego trybu z menu znajdującego się na górnym pasku aplikacji.

### Przyjęte założenia
* <b>Rysowanie</b> - pierwsza krawędź jest rysowana wyłącznie wtedy, gdy użytkwonik wykona ruch myszką z przytrzymanym lewym przyciskiem. Po narysowaniu pierwszej krawędzi kolejne można rysować przesuwając myszką z wciśniętym lewym przyciskiem, co gwarantuje nam podgląd obecnie rysowanie krawędzi, lub poprzez kliknięcie w dowolne miejsce na ekranie - wtedy poprzedni wierzchołek zostanie połączony z nowym autormatycznie. Ponadto program obsługuje mechanizm "inteligentego kończenia wielokąta", gdy rysowana krawędź znajdzie się w otoczeniu wierzhchołka początkowego. Ponadto rysowanie wielokąta można przerwać w trakcie i powrócić do niego później z innego trybu. Wtedy jednak zanim użytkownik narysuje kolejny wielokąt będzie zmuszczony do dokończenia poprzedniego rozpoczętej konstrukcji.
* <b>Relacje</b> - jedna krawędź może być jednocześnie wyłącznie w jednej relacji
* <b>Przeuswanie wielokąta</b> - rozpoczyna się po naciśnięciu na jedną z krawędzi wielokąta

### Opis algorytmu relacji
1. Określamy wierzchołek startowy, od którego rozpoczniemy sprawdzenie.
2. Najpierw wyruszamy w lewą stronę od startowego wierzchołka przyjmując za krawędź początkową krawędź , której jest on prawym punktem</br>
  I Sprawdzamy, czy obecnie relacje są zachowane, jeśli tak, przerywamy pętle i zwracamy wielokąt uzyskany w wyniku modyfikacji<br/>
  II Sprawdzamy czy dana krawędź jest w relacji z dowolną inną krawędzią.</br>
  III Jeśli nie jest, sprawdzamy, czy relacje w wielokącie się zachowane</br>
      a) Jeśli tak, zwracamy zmodyfikowany wielokąt.<br/>
      b) Jeśli nie, przechodzimy do punktu IV<br/>
  III Jeśli jest, poprawiamy relację względem drugiej krawędzi w relacji, czyli tak modyfikujemy naszą obecną krawędź by była prostopadła/równa krawędzi drugiej</br>
  IV Zmieniamy obecną krawędź na następną z lewej i wracamy do kroku I.
 3. Powtarzamy krok dwa, tyle że tym razem poruszamy się w prawo
 4. Jeżeli za drugim razem udało nam się również zakończyć pętle, zamieniamy nasz wielokąt wejściowy na ten, który uzyskaliśmy w wyniku modyfikacji.
 5. Jeśli natomiast żadna z pętli nie została przerwana po wykonaniu pełnego przejścia wokół wielokąta i powrotu do punktu początkowego uznajemy, że dana relacja nie może zostać zachowana.
 
 ## Autor
 [Ireneusz Stanicki](https://github.com/irek14), student Informatyki na wydziale Matematyki i Nauk Informacyjnych Politechniki Warszawskiej
