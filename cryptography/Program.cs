// See https://aka.ms/new-console-template for more information


var cryptography = new IShow[]
{
    new Cryptography(),
    new SymmetricEncryption(),
    new AsymmetricEncryption(),
    new MessageSignature()
};

foreach (var item in cryptography)
{
    item.Show();
}