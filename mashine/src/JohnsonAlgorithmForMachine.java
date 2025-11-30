import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

public class JohnsonAlgorithmForMachine {

    static int[][] timeTable;
    static int[] columnNumbers;
    static int n = 0, left = 0, right = 0;
    static List<Integer> remaining = new ArrayList<>();

    public static void printJohnsonAlgorithm(int[][] table) throws InterruptedException {

        timeTable = table;
        n = timeTable[0].length;

        // отстояния на индексите
        right = n - 1;
        left = 0;

        columnNumbers = new int[n];
        remaining = IntStream.range(0, n).boxed().collect(Collectors.toList());

        System.out.println("<- Първа форма на таблицата ->");
        printTable();

        System.out.println("<- Алгоритъм на Джонсън ->");

        while (!remaining.isEmpty()) {

            int minValue = Integer.MAX_VALUE;
            List<Integer> minIndexes = new ArrayList<>();

            // намираме минималната стойност
            for (int j : remaining) {
                int localMin = Math.min(timeTable[0][j], timeTable[1][j]);
                if (localMin < minValue)
                    minValue = localMin;
            }

            // всички индекси с тази стойност
            for (int j : remaining) {
                int localMin = Math.min(timeTable[0][j], timeTable[1][j]);
                if (localMin == minValue)
                    minIndexes.add(j);
            }

            // максимум два индекса
            if (minIndexes.size() > 2)
                minIndexes = minIndexes.subList(0, 2);

            List<Integer> processedIndexes = new ArrayList<>();

            // СЛУЧАЙ: има два минимални елемента
            if (minIndexes.size() > 1) {

                int idx1 = minIndexes.get(0);
                int idx2 = minIndexes.get(1);

                processedIndexes.add(idx1);
                processedIndexes.add(idx2);

                System.out.println("Намерени са две минимални стойности: " + minValue +
                        " (за заявки " + (idx1 + 1) + " и " + (idx2 + 1) + ")");

                boolean isIdx1Mach1 = timeTable[0][idx1] <= timeTable[1][idx1];
                boolean isIdx2Mach1 = timeTable[0][idx2] <= timeTable[1][idx2];

                // A: и двата са за машина 1 → отляво
                if (isIdx1Mach1 && isIdx2Mach1) {
                    if (timeTable[1][idx1] <= timeTable[1][idx2]) {
                        columnNumbers[left++] = idx1 + 1;
                        columnNumbers[left++] = idx2 + 1;
                        System.out.println("→ И двете отиват отляво в ред: "
                                + (idx1 + 1) + ", " + (idx2 + 1));
                    } else {
                        columnNumbers[left++] = idx2 + 1;
                        columnNumbers[left++] = idx1 + 1;
                        System.out.println("→ И двете отиват отляво в ред: "
                                + (idx2 + 1) + ", " + (idx1 + 1));
                    }
                }
                // Б: и двата са за машина 2 → отдясно
                else if (!isIdx1Mach1 && !isIdx2Mach1) {
                    if (timeTable[0][idx1] <= timeTable[0][idx2]) {
                        columnNumbers[right--] = idx1 + 1;
                        columnNumbers[right--] = idx2 + 1;
                        System.out.println("→ И двете отиват отдясно в ред: "
                                + (idx1 + 1) + ", " + (idx2 + 1));
                    } else {
                        columnNumbers[right--] = idx2 + 1;
                        columnNumbers[right--] = idx1 + 1;
                        System.out.println("→ И двете отиват отдясно в ред: "
                                + (idx2 + 1) + ", " + (idx1 + 1));
                    }
                }
                // В: единият е машина 1, другият – машина 2
                else {
                    int leftItem = isIdx1Mach1 ? idx1 : idx2;
                    int rightItem = isIdx1Mach1 ? idx2 : idx1;

                    columnNumbers[left++] = leftItem + 1;
                    columnNumbers[right--] = rightItem + 1;

                    System.out.println("→ " + (leftItem + 1) + " отива отляво");
                    System.out.println("→ " + (rightItem + 1) + " отива отдясно");
                }

            }
            // СЛУЧАЙ: има само един минимален елемент
            else {
                int idx = minIndexes.getFirst();
                processedIndexes.add(idx);

                System.out.println("Най-малкият елемент е: " + minValue +
                        " (за заявка " + (idx + 1) + ")");

                if (timeTable[0][idx] <= timeTable[1][idx]) {
                    columnNumbers[left++] = idx + 1;
                    System.out.println("→ Отива отляво");
                } else {
                    columnNumbers[right--] = idx + 1;
                    System.out.println("→ Отива отдясно");
                }
            }

            printCurrentSequence();
            Thread.sleep(3000);

            remaining.removeAll(processedIndexes);
            System.out.println();
        }

        System.out.println("<- Оптимална последователност ->");
        System.out.println(
                Arrays.stream(columnNumbers)
                        .mapToObj(String::valueOf)
                        .collect(Collectors.joining(" → "))
        );
    }

    private static void printCurrentSequence() {
        System.out.println("Текущо състояние на последователноста:");
        System.out.print("[ ");

        for (int columnNumber : columnNumbers) {
            if (columnNumber == 0) {
                System.out.print("_ ");
            } else {
                System.out.print(columnNumber + " ");
            }
        }
        System.out.println("]");
    }

    private static void printTable() {
        System.out.print("Номер на изделие                | j   | ");
        for (int j = 0; j < n; j++) {
            if (!remaining.contains(j))
                System.out.println(j + 1);
            else
                System.out.print(j + 1);

            System.out.print(" | ");
        }
        System.out.println();

        System.out.print("Време за обработка на I машина  | t1j | ");
        for (int j = 0; j < n; j++) {
            if (!remaining.contains(j))
                System.out.println(timeTable[0][j]);
            else
                System.out.print(timeTable[0][j]);

            System.out.print(" | ");
        }
        System.out.println();

        System.out.print("Време за обработка на II машина | t2j | ");
        for (int j = 0; j < n; j++) {
            if (!remaining.contains(j))
                System.out.println(timeTable[1][j]);
            else
                System.out.print(timeTable[1][j]);

            System.out.print(" | ");
        }
        System.out.println();
        System.out.println();
    }

}
