
class Shape { // ���� Ŭ����
	public Shape next; // �׸� 5-22�� �ڵ带 ���� �ʿ��� �κ�
	public Shape() { next = null; } // �׸� 5-22�� �ڵ带 ���� �ʿ��� �κ�
	
	public void draw() {
		System.out.println("Shape");
	}
}

class Line extends Shape {
	//@Override
	public void draw() { // �޼ҵ� �������̵�
		System.out.println("Line");
	}
}

class Rect extends Shape {
	//@Override
	public void draw() { // �޼ҵ� �������̵�
		System.out.println("Rect");
	}
}

class Circle extends Shape {
	//@Override
	public void draw() { // �޼ҵ� �������̵�
		System.out.println("Circle");
	}
}

public class MethodOverridingEx {
	
	public static void main(String[] args) {
		//Line line = new Line();
		Shape p;
		p=new Shape();
		p.draw();
		p=new Line();
		p.draw();
		p=new Rect();
		p.draw();
		p=new Circle();
		p.draw();
		
	}
}