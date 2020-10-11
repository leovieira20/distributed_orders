import 'dart:io';
import 'package:distributed_orders_flutter/consts.dart';
import 'package:dio/dio.dart';
import 'package:distributed_orders_flutter/domain/models/product.dart';

class ProductService {
  String _apiEndpoint = "api/product";

  Future<List<Product>> getAll() async {
    var endpoint = "http://${Platform.isAndroid ? kHostAddressAndroid : kHostAddressIos}:${5004}/$_apiEndpoint/create";
    var response = await Dio().get<List<Product>>(endpoint);
    return response.data;
  }
}
