import 'package:distributed_orders_flutter/pages/order_creation/order_creation_page.dart';
import 'package:distributed_orders_flutter/pages/order_list/order_list_vm.dart';
import 'package:flutter/material.dart';
import 'package:flutter_mobx/flutter_mobx.dart';

class OrderListPage extends StatefulWidget {
  static String routeName = "/orderListPage";

  final OrderListVm vm;

  OrderListPage(this.vm, {Key key}) : super(key: key);

  @override
  _OrderListPageState createState() => _OrderListPageState();
}

class _OrderListPageState extends State<OrderListPage> {
  @override
  void initState() {
    widget.vm.getOrders();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Orders"),
      ),
      body: Observer(
        builder: (_) {
          var orders = widget.vm.orders;

          if (orders == null) {
            return Center();
          }

          return ListView.builder(
            itemCount: orders.length,
            itemBuilder: (c, ix) {
              var order = orders[ix];

              return ListTile(
                title: Text(order.id),
              );
            },
          );
        },
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => Navigator.pushNamed(context, OrderCreationPage.routeName),
        child: Icon(Icons.add),
      ),
    );
  }
}
