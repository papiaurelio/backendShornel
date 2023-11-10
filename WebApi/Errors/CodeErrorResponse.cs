namespace WebApi.Errors
{
    public class CodeErrorResponse
    {
        public CodeErrorResponse(int statusCode, string message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El Request contiene errores",
                401 => "Autorización denegada",
                404 => "No se econtraron resultados",
                405 => "Solicitud no válida",
                415 => "Error de formato",
                500 => "Se ha producido un error en el servidor",
                _ => null
            };
        }
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
