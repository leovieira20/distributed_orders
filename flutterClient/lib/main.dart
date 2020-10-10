import 'package:distributed_orders_flutter/ioc.dart';
import 'package:distributed_orders_flutter/my_app.dart';
import 'package:flutter/material.dart';
import 'package:logger/logger.dart';

Logger logger = Logger();

void main() {
  setup();
  runApp(ioc<MyApp>());
}