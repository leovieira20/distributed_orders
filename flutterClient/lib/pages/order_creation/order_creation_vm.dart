import 'package:dio/dio.dart';
import 'package:distributed_orders_flutter/main.dart';
import 'package:flutter_guid/flutter_guid.dart';

class OrderCreationVm {
  createOrder() async {
    try {
      await Dio().post(
        "http://10.0.2.2:5000/api/order/create",
        data: {"Id": Guid.newGuid.toString()},
      );
    } catch (e) {
      logger.e("Error trying to create order");
    }
  }
}
