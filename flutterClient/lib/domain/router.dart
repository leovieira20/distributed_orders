import 'package:distributed_orders_flutter/pages/order_creation/order_creation_page.dart';
import 'package:distributed_orders_flutter/pages/order_list/order_list_page.dart';
import 'package:flutter/material.dart';
import '../ioc.dart';

Map<String, WidgetBuilder> getRoutes() {
  return {
    OrderListPage.routeName: (context) => ioc<OrderListPage>(),
    OrderCreationPage.routeName: (context) => ioc<OrderCreationPage>()
  };
}
