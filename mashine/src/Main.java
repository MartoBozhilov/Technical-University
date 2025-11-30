public class Main {

    public static void main(String[] args) throws InterruptedException {
        int[] t1 = {7, 8, 10, 3, 4, 11, 12, 15, 6, 8}; // Машина 1
        int[] t2 = {5, 9, 2, 5, 3, 10, 13, 9, 7, 10}; // Машина 2
        int[][] table = {t1, t2};

        JohnsonAlgorithmForMachine.printJohnsonAlgorithmSimple(table);
    }
}
