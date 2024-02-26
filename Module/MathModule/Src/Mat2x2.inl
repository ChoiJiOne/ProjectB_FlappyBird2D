#include "Vec2.h"

template<typename T>
inline TMat2x2<T> TMat2x2<T>::Zero()
{
	return TMat2x2<T>(
		static_cast<T>(0), static_cast<T>(0),
		static_cast<T>(0), static_cast<T>(0)
		);
}

template<typename T>
inline TMat2x2<T> TMat2x2<T>::Identity()
{
	return TMat2x2<T>(
		static_cast<T>(1), static_cast<T>(0),
		static_cast<T>(0), static_cast<T>(1)
		);
}

template<typename T>
inline TMat2x2<T> TMat2x2<T>::Transpose(const TMat2x2<T>& m)
{
	return TMat2x2<T>(
		m.e00, m.e10,
		m.e01, m.e11
		);
}

template<typename T>
inline T TMat2x2<T>::Determinant(const TMat2x2<T>& m)
{
	return m.e00 * m.e11 - m.e01 * m.e10;
}

template<typename T>
inline TMat2x2<T> TMat2x2<T>::Inverse(const TMat2x2<T>& m)
{
	T oneOverDeterminant = static_cast<T>(1) / Determinant(m);

	return TMat2x2<T>(
		+m.e11 * oneOverDeterminant,
		-m.e01 * oneOverDeterminant,
		-m.e10 * oneOverDeterminant,
		+m.e00 * oneOverDeterminant
		);
}