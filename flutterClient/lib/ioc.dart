import 'package:distributed_orders_flutter/my_app.dart';
import 'package:distributed_orders_flutter/pages/order_creation/order_creation.dart';
import 'package:distributed_orders_flutter/pages/order_creation/order_creation_vm.dart';
import 'package:get_it/get_it.dart';

GetIt ioc = GetIt.instance;

setup() {
  ioc.registerSingleton(MyApp());
  ioc.registerFactory(() => OrderCreationVm());
  ioc.registerFactory(() => OrderCreationPage(ioc<OrderCreationVm>()));
}