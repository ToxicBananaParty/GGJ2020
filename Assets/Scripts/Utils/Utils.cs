using System;

public class Utils {
	public static int indexForPoint(int x, int y, int width) {
		return (y * width) + x;
	}

	public static double degreesToRadians(double angle) {
		return Math.PI * angle / 180.0;
	}

	public static double radiansToDegrees(double angle) {
		return angle * (180.0 / Math.PI);
	}
}
