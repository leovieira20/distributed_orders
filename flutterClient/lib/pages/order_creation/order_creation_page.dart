import 'package:distributed_orders_flutter/pages/order_creation/order_creation_vm.dart';
import 'package:flutter/material.dart';

class OrderCreationPage extends StatefulWidget {
  static String routeName = "/orderCreationPage";

  final OrderCreationVm vm;

  OrderCreationPage(this.vm, {Key key}) : super(key: key);

  @override
  _OrderCreationPageState createState() => _OrderCreationPageState();
}

class _OrderCreationPageState extends State<OrderCreationPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Create Order"),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[

          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: widget.vm.createOrder,
        child: Icon(Icons.add),
      ),
    );
  }
}
