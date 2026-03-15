using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Google.Cloud.Firestore;
using System.Linq.Expressions;


namespace SpeechRecognition.Infra.FireStore.Repositories
{
    public static class FirestoreQueryBuilder
    {
        public static Query ApplyPredicate<TEntity>(
            Query query,
            Expression<Func<TEntity, bool>>? predicate)
        {
            if (predicate == null)
                return query;

            if (predicate.Body is BinaryExpression binary)
            {
                var member = (MemberExpression)binary.Left;
                var constant = (ConstantExpression)binary.Right;

                var field = member.Member.Name;
                var value = constant.Value;

                switch (binary.NodeType)
                {
                    case ExpressionType.Equal:
                        query = query.WhereEqualTo(field, value);
                        break;

                    case ExpressionType.GreaterThan:
                        query = query.WhereGreaterThan(field, value);
                        break;

                    case ExpressionType.LessThan:
                        query = query.WhereLessThan(field, value);
                        break;
                }
            }

            return query;
        }
    }
}
