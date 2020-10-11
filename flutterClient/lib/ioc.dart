import 'package:distributed_orders_flutter/domain/services/order_service.dart';
import 'package:distributed_orders_flutter/domain/services/product_service.dart';
import 'package:distributed_orders_flutter/my_app.dart';
import 'package:distributed_orders_flutter/pages/order_creation/order_creation_page.dart';
import 'package:distributed_orders_flutter/pages/order_creation/order_creation_vm.dart';
import 'package:distributed_orders_flutter/pages/order_list/order_list_page.dart';
import 'package:distributed_orders_flutter/pages/order_list/order_list_vm.dart';
import 'package:get_it/get_it.dart';

GetIt ioc = GetIt.instance;

setup() {
  ioc.registerSingleton(MyApp());
  ioc.registerSingleton(ProductService());
  ioc.registerSingleton(OrderService());

  ioc.registerFactory(() => OrderListVm(ioc<OrderService>()));
  ioc.registerFactory(() => OrderListPage(ioc<OrderListVm>()));

  ioc.registerFactory(() => OrderCreationVm(ioc<OrderService>(), ioc<ProductService>()));
  ioc.registerFactory(() => OrderCreationPage(ioc<OrderCreationVm>()));
}