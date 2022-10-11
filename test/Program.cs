using Refit;

namespace Teste {
    class MainClass {
        static async Task<string> Api(string[] args) {
            try {
                var contaClient = RestService.For<ContaApiService>("https://api.figma.com");
                //Console.WriteLine("Informe o seu token: ");

                //string token = Console.ReadLine().ToString();
                string token = "figd_DmVMdr9PnbfHlubrV0aNwRfIn91_LEUigip2CxxJ";
                
                var conta = await contaClient.GetContaAsync(token);
                return conta.email;
            } catch (Exception e) {
                return e.Message;
            }
        }
    }
}