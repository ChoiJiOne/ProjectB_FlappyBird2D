template<typename T>
inline T TVec3<T>::Dot(const TVec3<T>& lhs, const TVec3<T>& rhs)
{
	return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
}

template<typename T>
inline TVec3<T> TVec3<T>::Cross(const TVec3<T>& lhs, const TVec3<T>& rhs)
{
	return TVec3<T>(
		lhs.y * rhs.z - lhs.z * rhs.y,
		lhs.z * rhs.x - lhs.x * rhs.z,
		lhs.x * rhs.y - lhs.y * rhs.x
		);
}

template<typename T>
inline T TVec3<T>::LengthSquare(const TVec3<T>& v)
{
	return v.x * v.x + v.y * v.y + v.z * v.z;
}

template<typename T>
inline float TVec3<T>::Length(const TVec3<T>& v)
{
	float lengthSquare = static_cast<float>(TVec3<T>::LengthSquare(v));
	return std::sqrtf(lengthSquare);
}

template<typename T>
inline TVec3<T> TVec3<T>::Normalize(const TVec3<T>& v)
{
	float length = TVec3<T>::Length(v);

	return TVec3<T>(
		static_cast<T>(static_cast<float>(v.x) / length),
		static_cast<T>(static_cast<float>(v.y) / length),
		static_cast<T>(static_cast<float>(v.z) / length)
		);
}
