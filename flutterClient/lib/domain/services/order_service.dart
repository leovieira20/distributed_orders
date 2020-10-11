import 'dart:io';

import 'package:dio/dio.dart';
import 'package:distributed_orders_flutter/consts.dart';
import 'package:distributed_orders_flutter/domain/models/address.dart';
import 'package:distributed_orders_flutter/domain/models/order.dart';
import 'package:distributed_orders_flutter/main.dart';

class OrderService {
  String _orderListEndpoint = "http://${Platform.isAndroid ? kHostAddressAndroid : kHostAddressIos}:${5002}/api/order";
  String _orderManagementEndpoint = "http://${Platform.isAndroid ? kHostAddressAndroid : kHostAddressIos}:${5000}/api/order";

  Future<List<Order>> getOrders() async {
    try {
      var endpoint = "$_orderListEndpoint/0/20";
      var response = await Dio().get<List<dynamic>>(endpoint);
      var mapped = response.data.map((e) => Order.fromJson(e));
      return mapped.toList(growable: false);
    } catch (e) {
      logger.e("Error trying to create order");
      rethrow;
    }
  }

  Future createOrder() async {
    try {
      var endpoint = "$_orderManagementEndpoint/create";
      await Dio().post(endpoint);
    } catch (e) {
      logger.e("Error trying to create order");
    }
  }

  Future cancelOrder(String orderId) async {
    try {
      var endpoint = "$_orderManagementEndpoint/cancel";
      await Dio().post(endpoint, data: {
        orderId,
      });
    } catch (e) {
      logger.e("Error trying to create order");
    }
  }

  Future updateDeliveryAddress(String orderId, Address address) async {
    try {
      var endpoint = "$_orderManagementEndpoint/updateDeliveryAddress/$orderId}";
      await Dio().post(endpoint);
    } catch (e) {
      logger.e("Error trying to create order");
    }
  }

  Future updateOrderItems() async {
    try {
      var endpoint = "$_orderManagementEndpoint/create";
      await Dio().post(endpoint);
    } catch (e) {
      logger.e("Error trying to create order");
    }
  }
}
