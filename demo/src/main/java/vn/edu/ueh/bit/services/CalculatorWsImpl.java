package vn.edu.ueh.bit.services;

import jakarta.jws.WebService;

@WebService(endpointInterface = "vn.edu.ueh.bit.services.CalculatorWs")
public class CalculatorWsImpl implements CalculatorWs {

    @Override
    public int add(int num1, int num2) {
        return num1 + num2;
    }

    @Override
    public int subtract(int num1, int num2) {
        return num1 - num2;
    }

    @Override
    public int multiply(int num1, int num2) {
        return num1 * num2;
    }

    @Override
    public double divide(int num1, int num2) {
        // Kiểm tra lỗi chia cho 0 để tránh làm sập Service
        if (num2 == 0) {
            throw new ArithmeticException("Lỗi: Không thể chia cho 0!");
        }
        // Ép kiểu sang double để có kết quả chính xác (ví dụ 5/2 = 2.5)
        return (double) num1 / num2;
    }
}