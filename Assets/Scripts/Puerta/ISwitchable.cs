namespace DesignPatterns.DIP
{
    /// <summary>
    /// Interfaz para objetos que se pueden activar/desactivar (como una puerta automática).
    /// </summary>
    public interface ISwitchable
    {
        /// <summary> ¿Está activo/abierto? </summary>
        bool IsActive { get; }

        /// <summary> Abre o activa el objeto. </summary>
        void Activate();

        /// <summary> Cierra o desactiva el objeto. </summary>
        void Deactivate();
    }
}
