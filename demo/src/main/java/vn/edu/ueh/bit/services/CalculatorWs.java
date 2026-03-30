package vn.edu.ueh.bit.services;

import jakarta.jws.WebMethod;
import jakarta.jws.WebService;

@WebService
public interface CalculatorWs {
    @WebMethod
    int add(int num1, int num2);

    @WebMethod
    int subtract(int num1, int num2); // Phép trừ

    @WebMethod
    int multiply(int num1, int num2); // Phép nhân

    @WebMethod
    double divide(int num1, int num2); // Phép chia 
}