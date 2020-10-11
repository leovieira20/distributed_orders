import 'dart:io';
import 'package:dio/dio.dart';
import 'package:distributed_orders_flutter/consts.dart';
import 'package:distributed_orders_flutter/domain/services/order_service.dart';
import 'package:distributed_orders_flutter/domain/services/product_service.dart';
import 'package:distributed_orders_flutter/main.dart';
import 'package:flutter_guid/flutter_guid.dart';

class OrderCreationVm {
  String _apiEndpoint = "api/order";
  OrderService orderService;
  ProductService productService;

  OrderCreationVm(this.orderService, this.productService);

  createOrder() async {
    try {
      var endpoint = "http://${Platform.isAndroid ? kHostAddressAndroid : kHostAddressIos}:$kHostPort/$_apiEndpoint/create";
      await Dio().post(
        endpoint,
        data: {"Id": Guid.newGuid.toString()},
      );
    } catch (e) {
      logger.e("Error trying to create order");
    }
  }
}
