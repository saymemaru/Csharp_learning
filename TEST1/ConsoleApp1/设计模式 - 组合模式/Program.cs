using 设计模式_组合模式;

CompositeNode root1 = new CompositeNode("Root_1");
root1.Add(new LeafNode("Leaf_1"));
root1.Add(new LeafNode("Leaf_2"));

CompositeNode branch1 = new CompositeNode("Branch_1");
branch1.Add(new LeafNode("Leaf_11"));
branch1.Add(new LeafNode("Leaf_12"));

root1.Add(branch1);

CompositeNode branch11 = new CompositeNode("Branch_11");
branch11.Add(new LeafNode("Leaf_111"));
branch11.Add(new LeafNode("Leaf_112"));

branch1.Add(branch11);

root1.Add(new LeafNode("Leaf_3"));

root1.Display(1);