
class Shape { // 슈퍼 클래스
	public Shape next; // 그림 5-22의 코드를 위해 필요한 부분
	public Shape() { next = null; } // 그림 5-22의 코드를 위해 필요한 부분
	
	public void draw() {
		System.out.println("Shape");
	}
}

class Line extends Shape {
	//@Override
	public void draw() { // 메소드 오버라이딩
		System.out.println("Line");
	}
}

class Rect extends Shape {
	//@Override
	public void draw() { // 메소드 오버라이딩
		System.out.println("Rect");
	}
}

class Circle extends Shape {
	//@Override
	public void draw() { // 메소드 오버라이딩
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