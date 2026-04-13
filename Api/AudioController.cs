using Microsoft.AspNetCore.Mvc;

namespace MyVoice.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AudioController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadAudio()
        {
            try
            {
                if (Request.ContentLength == 0)
                {
                    return BadRequest("No audio data provided");
                }

                // Read the base64 audio data from the request body
                using var reader = new StreamReader(Request.Body);
                var base64Audio = await reader.ReadToEndAsync();

                if (string.IsNullOrEmpty(base64Audio))
                {
                    return BadRequest("Audio data is empty");
                }

                try
                {
                    // Convert base64 to byte array to validate
                    byte[] audioBytes = System.Convert.FromBase64String(base64Audio);

                    // Return success with the audio data size
                    return Ok(new
                    {
                        success = true,
                        size = audioBytes.Length,
                        message = "Audio received successfully"
                    });
                }
                catch (FormatException)
                {
                    return BadRequest("Invalid base64 format");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
