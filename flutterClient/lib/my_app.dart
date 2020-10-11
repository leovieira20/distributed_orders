import 'package:distributed_orders_flutter/domain/router.dart';
import 'package:distributed_orders_flutter/pages/order_list/order_list_page.dart';
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
      routes: getRoutes(),
      initialRoute: OrderListPage.routeName,
    );
  }
}