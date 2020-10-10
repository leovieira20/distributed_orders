import 'package:distributed_orders_flutter/ioc.dart';
import 'package:distributed_orders_flutter/pages/order_creation/order_creation.dart';
import 'package:flutter/material.dart';

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
        visualDensity: VisualDensity.adaptivePlatformDensity,
      ),
      home: ioc<OrderCreationPage>(),
    );
  }
}