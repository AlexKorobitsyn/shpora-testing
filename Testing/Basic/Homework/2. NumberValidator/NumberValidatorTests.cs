using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace HomeExercise.Tasks.NumberValidator;

[TestFixture]
public class NumberValidatorTests
{
    [Test]
     public void Constructor_InvalidDefinition_Fails()
    {
        Assert.Throws<ArgumentException>(() => new NumberValidator(-1, 2));
        Assert.Throws<ArgumentException>(() => new NumberValidator(0, 1));
    }
    [Test]
    public void Constructor_InvalidScale_Fail()
    {
        Assert.Throws<ArgumentException>(() => new NumberValidator(5, -1));
        Assert.Throws<ArgumentException>(() => new NumberValidator(5, 5));
        Assert.Throws<ArgumentException>(() => new NumberValidator(3, 4));
    }
    [Test]
    public void Constructor_ValidDefinition()
    {
        Assert.DoesNotThrow(() => new NumberValidator(1));
        Assert.DoesNotThrow(() => new NumberValidator(5, 2));
        Assert.DoesNotThrow(() => new NumberValidator(3, 1, true));
    }
    
    [Test]
    public void IsValidNumber_PositiveValidNumbers()
    {
        var validator = new NumberValidator(17, 2, true);
        Assert.Multiple(() =>
        {
            ClassicAssert.IsTrue(validator.IsValidNumber("0"));
            ClassicAssert.IsTrue(validator.IsValidNumber("0.0"));
            ClassicAssert.IsTrue(validator.IsValidNumber("12.3"));
            ClassicAssert.IsTrue(validator.IsValidNumber("+1.23"));
            ClassicAssert.IsTrue(validator.IsValidNumber("123"));
        });
    }
    [Test]
    public void IsValidNumber_NegativeInvalidNumbers()
    {
        var validator = new NumberValidator(3, 2, true);
        Assert.Multiple(() =>
        {
            ClassicAssert.IsFalse(validator.IsValidNumber("-1.23"));
            ClassicAssert.IsFalse(validator.IsValidNumber("-1.3"));
        });
    }
    [Test]
    public void IsValidNumber_LessPrecisionWithoutSign()
    {
        var validator = new NumberValidator(3, 2, true);
        Assert.Multiple(() =>
        {
            ClassicAssert.IsFalse(validator.IsValidNumber("00.00"));
            ClassicAssert.IsFalse(validator.IsValidNumber("0.001"));
        });
    }
    [Test]
    public void IsValidNumber_LessPrecisionWithSign()
    {
        var validator = new NumberValidator(3, 2, true);
        Assert.Multiple(() =>
        {
            ClassicAssert.IsFalse(validator.IsValidNumber("+10.0"));
            ClassicAssert.IsFalse(validator.IsValidNumber("-0.1"));
        });
    }
    [Test]
    public void IsValidNumber_NegativeNumbersWithoutOnlyPositiveFalse()
    {
        var validator = new NumberValidator(4, 2);
        Assert.Multiple(() =>
        {
            ClassicAssert.IsTrue(validator.IsValidNumber("-1.23"));
            ClassicAssert.IsTrue(validator.IsValidNumber("-3.23"));
        });
    }
    [Test]
    public void IsValidNumber_ExceptedCase()
    {
        var validator = new NumberValidator(4, 2);
        Assert.Multiple(() =>
        {
            ClassicAssert.IsFalse(validator.IsValidNumber(null));
            ClassicAssert.IsFalse(validator.IsValidNumber("a.sd"));
            ClassicAssert.IsFalse(validator.IsValidNumber("12."));
            ClassicAssert.IsFalse(validator.IsValidNumber(".34"));
            ClassicAssert.IsFalse(validator.IsValidNumber(""));

        });
    }
    
    [Test]
    public void IsValidNumber_DifferentMarks()
    {
        var validator = new NumberValidator(5, 2);
        Assert.Multiple(() =>
        {
            ClassicAssert.IsTrue(validator.IsValidNumber("12.34"));
            ClassicAssert.IsTrue(validator.IsValidNumber("12,34"));
        });
    }
    [Test]
    public void IsValidNumber_ScaleTrouble()
    {
        var validator = new NumberValidator(6, 2);
        Assert.Multiple(() =>
        {
            ClassicAssert.IsTrue(validator.IsValidNumber("10.25"));
            ClassicAssert.IsFalse(validator.IsValidNumber("5.234"));
        });
    }
    
    [Test]
    public void IsValidNumber_IntegerNumbers()
    {
        var validator = new NumberValidator(3, 0);
        Assert.Multiple(() =>
        {
            ClassicAssert.IsTrue(validator.IsValidNumber("123"));
            ClassicAssert.IsFalse(validator.IsValidNumber("12.3"));
            ClassicAssert.IsFalse(validator.IsValidNumber("12.0"));
        });
    }
}