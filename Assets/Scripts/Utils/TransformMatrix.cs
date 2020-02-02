using System;
using UnityEngine;

public class TransformMatrix {
	float[] m_matrix = new float[16];

	public TransformMatrix() {
		reset();
	}

	public TransformMatrix(TransformMatrix transform) {
		for(int i = 0; i<16; i++) {
			m_matrix[i] = transform.m_matrix[i];
		}
	}
		
	public TransformMatrix(float a00, float a01, float a02, float a10, float a11, float a12, float a20, float a21, float a22) {
		m_matrix[0] = a00; m_matrix[4] = a01; m_matrix[8] = 0; m_matrix[12] = a02;
		m_matrix[1] = a10; m_matrix[5] = a11; m_matrix[9] = 0; m_matrix[13] = a12;
		m_matrix[2] = 0; m_matrix[6] = 0; m_matrix[10] = 1; m_matrix[14] = 0;
		m_matrix[3] = a20; m_matrix[7] = a21; m_matrix[11] = 0; m_matrix[15] = a22;
	}

	public void apply(float a00, float a01, float a02, float a10, float a11, float a12, float a20, float a21, float a22) {
		m_matrix[0] = a00; m_matrix[4] = a01; m_matrix[8] = 0; m_matrix[12] = a02;
		m_matrix[1] = a10; m_matrix[5] = a11; m_matrix[9] = 0; m_matrix[13] = a12;
		m_matrix[2] = 0; m_matrix[6] = 0; m_matrix[10] = 1; m_matrix[14] = 0;
		m_matrix[3] = a20; m_matrix[7] = a21; m_matrix[11] = 0; m_matrix[15] = a22;
	}

	public void reset() {
		// Identity m_matrix
		m_matrix[0] = 1; m_matrix[4] = 0; m_matrix[8] = 0; m_matrix[12] = 0;
		m_matrix[1] = 0; m_matrix[5] = 1; m_matrix[9] = 0; m_matrix[13] = 0;
		m_matrix[2] = 0; m_matrix[6] = 0; m_matrix[10] = 1; m_matrix[14] = 0;
		m_matrix[3] = 0; m_matrix[7] = 0; m_matrix[11] = 0; m_matrix[15] = 1;
	}

	public float[] getMatrix() {
		return m_matrix;
	}

	public TransformMatrix getInverse() {
		// Compute the determinant
		float det = m_matrix[0] * (m_matrix[15] * m_matrix[5] - m_matrix[7] * m_matrix[13]) -
				m_matrix[1] * (m_matrix[15] * m_matrix[4] - m_matrix[7] * m_matrix[12]) +
				m_matrix[3] * (m_matrix[13] * m_matrix[4] - m_matrix[5] * m_matrix[12]);
			
		// Compute the inverse if the determinant is not zero
		// (don't use an epsilon because the determinant may *really* be tiny)
		if (det != 0) {
			return new TransformMatrix((m_matrix[15] * m_matrix[5] - m_matrix[7] * m_matrix[13]) / det,
								-(m_matrix[15] * m_matrix[4] - m_matrix[7] * m_matrix[12]) / det,
									(m_matrix[13] * m_matrix[4] - m_matrix[5] * m_matrix[12]) / det,
								-(m_matrix[15] * m_matrix[1] - m_matrix[3] * m_matrix[13]) / det,
									(m_matrix[15] * m_matrix[0] - m_matrix[3] * m_matrix[12]) / det,
								-(m_matrix[13] * m_matrix[0] - m_matrix[1] * m_matrix[12]) / det,
									(m_matrix[7]  * m_matrix[1] - m_matrix[3] * m_matrix[5])  / det,
								-(m_matrix[7]  * m_matrix[0] - m_matrix[3] * m_matrix[4])  / det,
									(m_matrix[5]  * m_matrix[0] - m_matrix[1] * m_matrix[4])  / det);
		}
		else {
			return new TransformMatrix();
		}
	}

	public Vector2 transform(float x, float y) {
		return new Vector2(m_matrix[0] * x + m_matrix[4] * y + m_matrix[12],
							m_matrix[1] * x + m_matrix[5] * y + m_matrix[13]);
	}

	public Vector2 transform(Vector2 point) {
		return new Vector2(m_matrix[0] * point.x + m_matrix[4] * point.y + m_matrix[12],
							m_matrix[1] * point.x + m_matrix[5] * point.y + m_matrix[13]);
	}

	public Rect transform(Rect rectangle) {
		// Transform the 4 corners of the rectangle
		Vector2[] points = {
			transform(rectangle.x, rectangle.y),
			transform(rectangle.x, rectangle.y + rectangle.height),
			transform(rectangle.x + rectangle.width, rectangle.y),
			transform(rectangle.x + rectangle.width, rectangle.y + rectangle.height)
		};

		// Compute the bounding rectangle of the transformed points
		float left = points[0].x;
		float top = points[0].y;
		float right = points[0].x;
		float bottom = points[0].y;
		for(int i = 1; i<4; ++i) {
			if (points[i].x<left) {
				left = points[i].x;
			}
			else if (points[i].x > right) {
				right = points[i].x;
			}
			if (points[i].y<top) {
				top = points[i].y;
			}
			else if (points[i].y > bottom) {
				bottom = points[i].y;
			}
		}
		return new Rect(left, top, right - left, bottom - top);
	}

	public Vector2[] transform(Vector2[] polygon) {
		Vector2[] newPoly = new Vector2[polygon.Length];
		int i = 0;
		foreach(var point in polygon) {
			newPoly[i] = transform(point);
			i++;
		}
		return newPoly;
	}
	
	public TransformMatrix combine(TransformMatrix transform) {
		float[] a = m_matrix;
		float[] b = transform.m_matrix;

		this.apply(a[0] * b[0] + a[4] * b[1] + a[12] * b[3],
				a[0] * b[4] + a[4] * b[5] + a[12] * b[7],
				a[0] * b[12] + a[4] * b[13] + a[12] * b[15],
				a[1] * b[0] + a[5] * b[1] + a[13] * b[3],
				a[1] * b[4] + a[5] * b[5] + a[13] * b[7],
				a[1] * b[12] + a[5] * b[13] + a[13] * b[15],
				a[3] * b[0] + a[7] * b[1] + a[15] * b[3],
				a[3] * b[4] + a[7] * b[5] + a[15] * b[7],
				a[3] * b[12] + a[7] * b[13] + a[15] * b[15]);
		return this;
	}

	public TransformMatrix translate(float x, float y) {
		var translation = new TransformMatrix(1, 0, x,
											  0, 1, y,
											  0, 0, 1);
		return combine(translation);
	}

	public TransformMatrix translate(Vector2 offset) {
		var translation = new TransformMatrix(1, 0, offset.x,
											 0, 1, offset.y,
											 0, 0, 1);
		return combine(translation);
	}

	public TransformMatrix rotate(float degrees) {
		float rad = (float)Utils.degreesToRadians((double)degrees);
		float cos = (float)Math.Cos(rad);
		float sin = (float)Math.Sin(rad);

		var rotation = new TransformMatrix(cos, -sin, 0,
										sin,  cos, 0,
										0,	  0,   1);

		return combine(rotation);
	}

	public TransformMatrix rotate(float  degrees, float  centerX, float  centerY) {
		float rad = (float)Utils.degreesToRadians((double)degrees);
		float cos = (float)Math.Cos(rad);
		float sin = (float)Math.Sin(rad);

		var rotation = new TransformMatrix(cos, -sin, centerX * (1 - cos) + centerY * sin,
										sin,  cos, centerY * (1 - cos) - centerX * sin,
										0,	  0,   1);

		return combine(rotation);
	}

	public TransformMatrix rotate(float  degrees, Vector2 center) {
		float rad = (float)Utils.degreesToRadians((double)degrees);
		float cos = (float)Math.Cos(rad);
		float sin = (float)Math.Sin(rad);

		var rotation = new TransformMatrix(cos, -sin, center.x * (1 - cos) + center.y * sin,
										sin,  cos, center.y * (1 - cos) - center.x * sin,
										0,	  0,   1);

		return combine(rotation);
	}

	public TransformMatrix scale(float scaleX, float scaleY) {
		var scaling = new TransformMatrix(scaleX, 0,		 0,
									 0,		 scaleY, 0,
									 0,		 0,		 1);

		return combine(scaling);
	}

	public TransformMatrix scale(float  scaleX, float  scaleY, float  centerX, float  centerY) {
		var scaling = new TransformMatrix(scaleX, 0,		 centerX * (1 - scaleX),
									 0,		 scaleY, centerY * (1 - scaleY),
									 0,		 0,		 1);

		return combine(scaling);
	}

	public TransformMatrix scale(Vector2 factors) {
		var scaling = new TransformMatrix(factors.x, 0,			0,
									 0,			factors.y,	0,
									 0,			0,			1);

		return combine(scaling);
	}

	public TransformMatrix scale(Vector2 factors, Vector2 center) {
		var scaling = new TransformMatrix(factors.x, 0,			center.x * (1 - factors.x),
									 0,			factors.y,	center.y * (1 - factors.y),
									 0,			0,			1);

		return combine(scaling);
	}

	public override String ToString() {
			return "Transform(["+
				m_matrix[0]+", "+m_matrix[4]+", "+m_matrix[8]+", "+m_matrix[12]+"], ["+
				"["+m_matrix[1]+", "+m_matrix[5]+", "+m_matrix[9]+", "+m_matrix[13]+"], ["+
				"["+m_matrix[2]+", "+m_matrix[6]+", "+m_matrix[10]+", "+m_matrix[14]+"], ["+
				"["+m_matrix[3]+", "+m_matrix[7]+", "+m_matrix[11]+", "+m_matrix[15]+"])";
		}
}
