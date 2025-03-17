using Sirenix.Utilities;
using System;
using System.Collections.Generic;

public static class EnumUpdateButton
{
    public static void UpdateData<T, Data>(ref List<Data> list, string memberName) where T : System.Enum where Data : class, new()
    {
        int typesNum = Enum.GetNames(typeof(T)).Length;

        for (int i = 0; i < typesNum; i++)
        {
            var value = (T)(object)i;

            var data = GetEnumFromList(value, ref list, memberName);

            if (data == null)
            {
                Data newData = new();
                var memberInfoArray = newData.GetType().GetMember(memberName);
                memberInfoArray[0].SetMemberValue(newData, value);
                list.Insert(i, newData);
            }
        }
    }
    private static Data GetEnumFromList<T, Data>(T expectedValue, ref List<Data> list, string memberName) where Data : class
    {
        foreach (var data in list)
        {
            var memberInfoArray = data.GetType().GetMember(memberName);

            if (memberInfoArray[0].GetMemberValue(data).Equals(expectedValue))
            {
                return data;
            }
        }
        return null;
    }
}
