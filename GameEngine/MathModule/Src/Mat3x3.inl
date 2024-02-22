#include "Vec3.h"

template<typename T>
inline TMat3x3<T> TMat3x3<T>::Zero()
{
	return TMat3x3<T>(
		static_cast<T>(0), static_cast<T>(0), static_cast<T>(0),
		static_cast<T>(0), static_cast<T>(0), static_cast<T>(0),
		static_cast<T>(0), static_cast<T>(0), static_cast<T>(0)
		);
}

template<typename T>
inline TMat3x3<T> TMat3x3<T>::Identity()
{
	return TMat3x3<T>(
		static_cast<T>(1), static_cast<T>(0), static_cast<T>(0),
		static_cast<T>(0), static_cast<T>(1), static_cast<T>(0),
		static_cast<T>(0), static_cast<T>(0), static_cast<T>(1)
		);
}

template<typename T>
inline TMat3x3<T> TMat3x3<T>::Transpose(const TMat3x3<T>& m)
{
	return TMat3x3<T>(
		m.e00, m.e10, m.e20,
		m.e01, m.e11, m.e21,
		m.e02, m.e12, m.e22
		);
}

template<typename T>
inline T TMat3x3<T>::Determinant(const TMat3x3<T>& m)
{
	return m.e00 * (m.e11 * m.e22 - m.e21 * m.e12) - m.e10 * (m.e01 * m.e22 - m.e21 * m.e02) + m.e20 * (m.e01 * m.e12 - m.e11 * m.e02);
}

template<typename T>
inline TMat3x3<T> TMat3x3<T>::Inverse(const TMat3x3<T>& m)
{
	T oneOverDeterminant = static_cast<T>(1) / Determinant(m);

	return TMat3x3<T>(
		+(m.e11 * m.e22 - m.e21 * m.e12) * oneOverDeterminant,
		-(m.e10 * m.e22 - m.e20 * m.e12) * oneOverDeterminant,
		+(m.e10 * m.e21 - m.e20 * m.e11) * oneOverDeterminant,
		-(m.e01 * m.e22 - m.e21 * m.e02) * oneOverDeterminant,
		+(m.e00 * m.e22 - m.e20 * m.e02) * oneOverDeterminant,
		-(m.e00 * m.e21 - m.e20 * m.e01) * oneOverDeterminant,
		+(m.e01 * m.e12 - m.e11 * m.e02) * oneOverDeterminant,
		-(m.e00 * m.e12 - m.e10 * m.e02) * oneOverDeterminant,
		+(m.e00 * m.e11 - m.e10 * m.e01) * oneOverDeterminant
		);
}