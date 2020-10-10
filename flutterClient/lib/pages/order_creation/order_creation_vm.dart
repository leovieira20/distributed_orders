import 'dart:io';
import 'package:dio/dio.dart';
import 'package:distributed_orders_flutter/main.dart';
import 'package:flutter_guid/flutter_guid.dart';

const kHostPort = "5000";
const kHostAddressAndroid = "10.0.2.2";
const kHostAddressIos = "localhost";

class OrderCreationVm {
  String _apiEndpoint = "api/order";

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
